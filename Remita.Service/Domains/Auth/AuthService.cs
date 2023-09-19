using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Remita.Data.Interfaces;
using Remita.Models.DatabaseContexts;
using Remita.Models.Domains.User.Enums;
using Remita.Models.Entities.Domians.User;
using Remita.Models.Exceptions;
using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Constants = Remita.Models.Commons.Constants;

namespace Remita.Services.Domains.Auth;
public class AuthService : IAuthService
{
    /* private readonly UserManager<ApplicationUser> _userManager;
     private readonly JwtConfig _jwtConfig;
     private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
     private readonly INotificationManagerService _notificationManagerService;
     private readonly IOtpCodeService _otpCodeService;*/

    //private TimeSpan RefreshTokenValidity = TimeSpan.FromDays(7);


    /* public AuthenticationService(
         UserManager<ApplicationUser> userManager,
         JwtConfig jwtConfig,
         IUnitOfWork<ApplicationDbContext> unitOfWork,
         INotificationManagerService notificationManagerService,
         IOtpCodeService otpCodeService)
     {
         _userManager = userManager;
         _jwtConfig = jwtConfig;
         _unitOfWork = unitOfWork;
         _notificationManagerService = notificationManagerService;
         _otpCodeService = otpCodeService;
     }*/

    /*  public async Task<ServiceResponse<SignedInDto>> SignUpAsync(Guid hostelId, SignUpDto model, CancellationToken cancellationToken)
      {
          ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);
          if (user != null)
          {
              return new ServiceResponse<SignedInDto>
              {
                  Message = "An account already exists with this email.",
                  StatusCode = HttpStatusCode.BadRequest,
              };
          }

          if (!string.Equals(model.Password, model.ConfirmPassword))
          {
              return new ServiceResponse<SignedInDto>
              {
                  Message = "Password mismatch",
                  StatusCode = HttpStatusCode.BadRequest
              };
          }

          user = new ApplicationUser
          {
              Email = model.Email,
              UserName = model.Email
          };

          IdentityResult userCreationResult = await _userManager.CreateAsync(user, model.Password);
          if (!userCreationResult.Succeeded)
          {
              string? message = userCreationResult.Errors.Select(e => e.Description).FirstOrDefault();
              return new ServiceResponse<SignedInDto>
              {
                  Message = $"Account creation failed with reason: {message}",
                  StatusCode = HttpStatusCode.BadRequest
              };
          }

          userCreationResult = await _userManager.AddToRoleAsync(user, UserRole.User.ToString());
          if (!userCreationResult.Succeeded)
          {
              string? message = userCreationResult.Errors.Select(e => e.Description).FirstOrDefault();
              return new ServiceResponse<SignedInDto>
              {
                  Message = $"Account creation failed with reason: {message}",
                  StatusCode = HttpStatusCode.BadRequest
              };
          }

          CreateOtpNotificationDto otpNotification = new(user.Id, user.Email!, user.GetFullName(), hostelId, OtpOperation.EmailConfirmation);
          await _notificationManagerService.CreateOtpNotificationAsync(otpNotification, cancellationToken);

          SignedInDto result = await CreateAccessTokenAsync(user);

          return new ServiceResponse<SignedInDto>
          {
              StatusCode = HttpStatusCode.OK,
              Data = result
          };
      }


      public async Task<ServiceResponse<SignedInDto>> SignIn(Guid hostelId, SignInDto model, CancellationToken cancellationToken)
      {
          try
          {
              var email = model.Email.Trim();
              var password = model.Password.Trim();

              ApplicationUser? user = await _userManager.FindByEmailAsync(email);
              if (user == null)
              {
                  throw new AuthenticationException("Invalid login credentials");
              }

              bool verifypasswordResult = await _userManager.CheckPasswordAsync(user, password);
              if (!verifypasswordResult)
              {
                  throw new AuthenticationException("Invalid login credentials");
              }

              if (user.DeActivated)
              {
                  throw new AuthenticationException("Invalid login credentials");
              }

              SignedInDto result = await CreateAccessTokenAsync(user);

              if (!user.EmailConfirmed)
              {
                  CreateOtpNotificationDto otpNotification = new(user.Id, user.Email!, user.GetFullName(), hostelId, OtpOperation.EmailConfirmation);
                  await _notificationManagerService.CreateOtpNotificationAsync(otpNotification, cancellationToken);
              }

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
      }


      public async Task<ServiceResponse<SignedInDto>> RefreshAccessTokenAsync(string accessToken, string refreshToken)
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
      }


      public async Task<ServiceResponse> SendEmailConfirmationOtpAsync(Guid hostelId, VerifyEmailOtpDto model, CancellationToken cancellationToken)
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

          CreateOtpNotificationDto otpNotification = new(user.Id, user.Email!, user.GetFullName(), hostelId, OtpOperation.EmailConfirmation);
          await _notificationManagerService.CreateOtpNotificationAsync(otpNotification, cancellationToken);

          return new ServiceResponse
          {
              StatusCode = HttpStatusCode.OK,
          };
      }

      public async Task<ServiceResponse> SendForgotPasswordOtpAsync(Guid hostelId, ForgotPasswordOtpDto model, CancellationToken cancellationToken)
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
      }

      public async Task<ServiceResponse> VerifyEmailAsync(VerifyEmailDto model)
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

          bool isOtpValid = await _otpCodeService.VerifyOtpAsync(user.Id, model.Otp, OtpOperation.EmailConfirmation);

          if (!isOtpValid)
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


      public async Task<ServiceResponse> ResetPasswordAsync(ResetPasswordDto model)
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
      }


      private async Task<SignedInDto> CreateAccessTokenAsync(ApplicationUser? user)
      {
          if (user == null)
          {
              throw new AuthenticationException("Invalid login credentials");
          }

          JwtSecurityTokenHandler jwTokenHandler = new JwtSecurityTokenHandler();
          string? secretKey = _jwtConfig.JwtKey;
          byte[] key = Encoding.ASCII.GetBytes(secretKey);

          var claims = new List<Claim>
              {
                  new Claim("UserId", user.Id),
                  new Claim(ClaimTypes.Name, user.UserName!),
                  new Claim(ClaimTypes.NameIdentifier, user.Id),
                  new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                  new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                  new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
              };

          var tokenDescriptor = new SecurityTokenDescriptor
          {
              Subject = new ClaimsIdentity(claims),
              Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.JwtExpireMinutes),
              SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
          };

          var securityToken = jwTokenHandler.CreateToken(tokenDescriptor);
          var accessToken = jwTokenHandler.WriteToken(securityToken);
          var refreshToken = await GenerateRefreshTokenAsync(user.Id);

          await _unitOfWork.SaveChangesAsync();

          return new SignedInDto(accessToken, refreshToken, tokenDescriptor.Expires.Value.ToTimeStamp());
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
      }*/

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly JwtConfig _jwtConfig;
    public AuthService(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager, IUnitOfWork<ApplicationDbContext> unitOfWork,
        IConfiguration configuration, JwtConfig jwtConfig)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _jwtConfig = jwtConfig;
    }

    public Task<ServiceResponse> ConfirmEmailAsync(ConfirmEmailDto otpDto)
    {
        throw new NotImplementedException();
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

    public Task<ServiceResponse<AuthenticationResponse>> RefreshAccessTokenAsync(string accessToken, string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> SendEmailConfirmationOtpAsync(ConfirmationEmailOtpDto otpDto)
    {
        throw new NotImplementedException();
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

    public Task<ServiceResponse> ForgotPasswordAsync(string email)
    {
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

    public Task<ServiceResponse> ResetPasswordAsync(ResetPasswordRequest request)
    {
        throw new NotImplementedException();
    }
}


