﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Remita.Cache.Interfaces;
using Remita.Data.Interfaces;
using Remita.Models.DatabaseContexts;
using Remita.Models.Domains.Security;
using Remita.Models.Domains.User.Enums;
using Remita.Models.Entities.Domians.User;
using Remita.Models.Exceptions;
using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Domains.Security;
using Remita.Services.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Constants = Remita.Models.Commons.Constants;

namespace Remita.Services.Domains.Auth;
public class AuthService : IAuthService
{
    private readonly ICacheService _cacheService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly JwtConfig _jwtConfig;
    private TimeSpan RefreshTokenValidity = TimeSpan.FromDays(7);
    private TimeSpan ExpirationTime = TimeSpan.FromHours(1);
    private const int MAX_RECURRENT_FAILED_SIGN_IN_ATTEMPT = 5;
    public AuthService(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager, IUnitOfWork<ApplicationDbContext> unitOfWork,
        IConfiguration configuration, JwtConfig jwtConfig, ICacheService cacheService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _jwtConfig = jwtConfig;
        _cacheService = cacheService;
    }
    public async Task<ServiceResponse<AccountResponse>> CreateUser(UserRegistrationRequest request)
    {
        try
        {
            ApplicationUser? existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new AuthenticationException($"User already exists with Email {request.Email}");
            }

            existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new AuthenticationException($"User already exists with username {request.UserName}");
            }
            var roleName = Constants.DefaultRoleName;
            ApplicationRole? userRole = await _roleManager.FindByNameAsync(roleName);

            ApplicationUser user = new()
            {

                Email = request.Email.ToLower(),
                UserName = request.UserName.Trim().ToLower(),
                MiddleName = request.MiddleName,
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                MatricNumber = request.MatricNumber,
                Department = request.Department,
                PhoneNumber = request.MobileNumber,
                Active = true,
                UserType = userRole!.Type
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var message = $"Failed to create user: {result.Errors.FirstOrDefault()?.Description}";
                throw new AuthenticationException(message);
            }

            string? role = user.UserType.ToString();
            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (roleExist)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                await _roleManager.CreateAsync(new ApplicationRole(role));
            }

            var AccountResult = new AccountResponse()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Success = true,
                Message = "your account has been created"

            };
            return new ServiceResponse<AccountResponse>
            {
                StatusCode = HttpStatusCode.Created,
                Data = AccountResult
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<AccountResponse>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<AuthenticationResponse>> UserLogin(LoginRequest request)
    {
        try
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(request.UserName.ToLower().Trim());

            if (user == null)
            {
                throw new AuthenticationException("Invalid username or password");
            }


            if (!user.Active)
            {
                throw new AuthenticationException("Account is not active");
            }


            bool result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                throw new AuthenticationException("Invalid username or password");
            }

            JwtToken userToken = GenerateJwtToken(user);

            string? userType = user.UserType.GetStringValue();

            string fullName = string.IsNullOrWhiteSpace(user.MiddleName)
                ? $"{user.LastName} {user.FirstName}"
                : $"{user.LastName} {user.FirstName} {user.MiddleName}";


            var authResult = new AuthenticationResponse { JwtToken = userToken, UserType = userType, FullName = fullName, TwoFactor = false, UserId = user.Id };
            return new ServiceResponse<AuthenticationResponse>()
            {
                Data = authResult,
                StatusCode = HttpStatusCode.OK,
                Message = "Logged In"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<AuthenticationResponse>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = ex.Message
            };
        }
    }
    public async Task<ServiceResponse> SendEmailConfirmationOtpAsync(ConfirmationEmailOtpDto otpDto)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(otpDto.Email);
        if (user == null)
        {
            return new ServiceResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Account does not exist"
            };
        }

        CreateOtpNotificationDto otpNotification = new(user.Id, user.Email!, user.GetFullName(), hostelId, OtpOperation.EmailConfirmation);
        await _notificationManagerService.CreateOtpNotificationAsync(otpNotification, cancellationToken);

        return new ServiceResponse
        {
            StatusCode = HttpStatusCode.OK,
        };
        throw new NotImplementedException();
    }
    public async Task<ServiceResponse> ConfirmEmailAsync(ConfirmEmailDto model)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return new ServiceResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Account does not exist"
            };
        }
        string cacheKey = CacheKeySelector.OtpCodeCacheKey(user.Id, OtpOperation.EmailConfirmation);
        ConfirmEmailDto? isOtpValid = await _cacheService.ReadFromCache<ConfirmEmailDto>(cacheKey);
        if (isOtpValid == null)
        {
            return new ServiceResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Invalid OTP"
            };
        }


        user.EmailConfirmed = true;
        IdentityResult res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded)
        {
            string errorMessage = res.Errors.Select(e => e.Description).First();
            return new ServiceResponse
            {
                Message = $"Email confirmation failed with failure reason - {errorMessage}",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        return new ServiceResponse
        {
            StatusCode = HttpStatusCode.OK,
        };
    }

    public async Task<ServiceResponse> ForgotPasswordAsync(string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return new ServiceResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Account does not exist"
            };
        }

        CreateOtpNotificationDto otpNotification = new(user.Id, user.Email!, user.GetFullName(), hostelId, OtpOperation.PasswordReset);
        await _notificationManagerService.CreateOtpNotificationAsync(otpNotification, cancellationToken);

        return new ServiceResponse
        {
            StatusCode = HttpStatusCode.OK,
        };
        throw new NotImplementedException();
    }
    public Task<ServiceResponse> ResetPasswordAsync(ResetPasswordRequest request)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return new ServiceResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Account does not exist"
            };
        }

        if (!string.Equals(model.Password, model.ConfirmPassword))
        {
            return new ServiceResponse
            {
                Message = "Password mismatch",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        bool isOtpValid = await _otpCodeService.VerifyOtpAsync(user.Id, model.Otp, OtpOperation.PasswordReset);

        if (!isOtpValid)
        {
            return new ServiceResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Invalid OTP"
            };
        }

        IdentityResult res = await _userManager.RemovePasswordAsync(user);
        if (!res.Succeeded)
        {
            string errorMessage = res.Errors.Select(e => e.Description).First();
            return new ServiceResponse
            {
                Message = $"Password reset failed with failure reason - {errorMessage}",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        res = await _userManager.AddPasswordAsync(user, model.Password);
        if (!res.Succeeded)
        {
            string errorMessage = res.Errors.Select(e => e.Description).First();
            return new ServiceResponse
            {
                Message = $"Password reset failed with failure reason - {errorMessage}",
                StatusCode = HttpStatusCode.BadRequest
            };
        }


        return new ServiceResponse
        {
            StatusCode = HttpStatusCode.OK,
        };
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<AuthenticationResponse>> RefreshAccessTokenAsync(string accessToken, string refreshToken)
    {
        try
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal.Identity is null)
            {
                throw new AuthenticationException("Access has expired");

            }

            string email = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Email).Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new AuthenticationException("Access has expired");

            }

            bool isRefreshTokenValid = user == null ||
                                       user.RefreshToken != refreshToken ||
                                       user.RefreshTokenExpiryTime <= DateTime.UtcNow;

            if (isRefreshTokenValid)
            {
                throw new AuthenticationException("Access has expired");
            }

            var result = await CreateAccessTokenAsync(user);
            return new ServiceResponse<SignedInDto>
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            };
        }
        catch (AuthenticationException ex)
        {
            return new ServiceResponse<SignedInDto>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = ex.Message
            };
        }
        throw new NotImplementedException();
    }

    private JwtToken GenerateJwtToken(ApplicationUser user, string expires = null, List<Claim> additionalClaims = null)
    {
        JwtSecurityTokenHandler jwtTokenHandler = new();
        // string jwtConfig = _configuration.GetSection("JwtConfig:JwtKey").Value!;

        var key = Encoding.ASCII.GetBytes(_jwtConfig.JwtKey);
        string userRole = user.UserType.GetStringValue()!;

        IdentityOptions _options = new();

        var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName!),
                new Claim(ClaimTypes.Role, userRole!)
        };

        if (additionalClaims != null)
        {
            claims.AddRange(additionalClaims);
        }

        string issuer = _jwtConfig.JwtIssuer;
        string audience = _jwtConfig.JwtAudience;
        string expire = _jwtConfig.JwtExpireMinutes;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = string.IsNullOrWhiteSpace(expires)
                ? DateTime.Now.AddHours(double.Parse(expire))
                : DateTime.Now.AddMinutes(double.Parse(expires)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            Issuer = issuer,
            Audience = audience
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return new JwtToken
        {
            Token = jwtToken,
            Issued = DateTime.Now,
            Expires = tokenDescriptor.Expires
        };
    }
    private async Task<string> GenerateRefreshTokenAsync(string userId)
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber);
            IEnumerable<RefreshToken> refreshTokens = await _unitOfWork.GetRepository<RefreshToken>()
                                                                 .GetQueryableList(x => x.UserId == userId && x.IsActive).ToListAsync();

            foreach (var token in refreshTokens)
            {
                token.IsActive = false;
            }
            _unitOfWork.GetRepository<RefreshToken>().Add(new RefreshToken
            {
                CreatedAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(RefreshTokenValidity),
                IsActive = true,
                UserId = userId,
                Token = refreshToken
            });
            return refreshToken;
        }
    }
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        string secretKey = _jwtConfig.JwtKey;
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = true,
            ValidIssuer = _jwtConfig.JwtIssuer,
            ValidAudience = _jwtConfig.JwtAudience
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
            )
        {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }

}


