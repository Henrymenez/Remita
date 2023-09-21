using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Remita.Cache.Interfaces;
using Remita.Data.Interfaces;
using Remita.Models.DatabaseContexts;
using Remita.Models.Entities.Domians.User;
using Remita.Services.Domains.Admin.Dtos;
using Remita.Services.Domains.Auth;
using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Domains.OutboundNotifications;
using Remita.Services.Domains.Security;
using Remita.Services.Domains.User;
using Remita.Services.Domains.User.Dtos;
using Remita.Services.Utility;
using System.Net;

namespace Remita.Services.Domains.Admin;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
    private readonly IRepository<ApplicationUser> _userRepo;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly JwtConfig _jwtConfig;
    private readonly ICacheService _cacheService;
    private readonly IAccountLockoutService _accountLockoutService;
    private readonly INotificationManagerService _notificationManagerService;
    private readonly IOtpCodeService _otpCodeService;

    public AdminService(IUnitOfWork<ApplicationDbContext> unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager, JwtConfig jwtConfig, ICacheService cacheService, IAccountLockoutService accountLockoutService,
         INotificationManagerService notificationManagerService, IOtpCodeService otpCodeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepo = _unitOfWork.GetRepository<ApplicationUser>();
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtConfig = jwtConfig;
        _cacheService = cacheService;
        _accountLockoutService = accountLockoutService;
        _notificationManagerService = notificationManagerService;
        _otpCodeService = otpCodeService;
    }

    public async Task<ServiceResponse> ActivateUser(string email)
    {
        var user = await _userRepo.SingleAsync(u => u.Email == email);
        if (user == null)
        {
            return new ServiceResponse()
            {
                Message = "User Not Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }
        user.Active = true;
        _userRepo.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return new ServiceResponse()
        {
            Message = "User Account Activated",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ServiceResponse<AccountResponse>> CreateNewUser(AdminUserRegistrationDto request)
    {
        var authService = new AuthService(_userManager, _roleManager, _unitOfWork, _jwtConfig, _cacheService, _accountLockoutService,
        _notificationManagerService, _otpCodeService);
        var user = new UserRegistrationRequest()
        {
            Email = request.Email,
            Password = request.Password,
            MobileNumber = request.Password,
            UserName = request.Username,
            FirstName = request.Firstname,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Department = request.Department,
            MatricNumber = request.MatricNumber
        };
        var result = await authService.CreateUser(user);
        if (result == null)
        {
            return new ServiceResponse<AccountResponse>()
            {
                Message = "Unable to register",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
        result.StatusCode = HttpStatusCode.OK;
        return result;
    }

    public async Task<ServiceResponse> DeleteUser(string email)
    {
        var user = await _userRepo.SingleAsync(u => u.Email == email);
        if (user == null)
        {
            return new ServiceResponse()
            {
                Message = "User Not Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }
        user.Active = false;
        _userRepo.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return new ServiceResponse()
        {
            Message = "User Account Deleted",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ServiceResponse<UserResponse>> UpdateUser(string email, UpdateUserDto request)
    {
        var user = await _unitOfWork.GetRepository<ApplicationUser>().SingleAsync(u => u.Email == email);
        if (user == null)
        {
            return new ServiceResponse<UserResponse>()
            {
                Data = null,
                Message = "User Not Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }
        user.FirstName = request.Firstname;
        user.MiddleName = request.MiddleName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.MobileNumber;
        _unitOfWork.GetRepository<ApplicationUser>().Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new ServiceResponse<UserResponse>()
        {
            Data = new UserResponse()
            {
                Success = true,
                UserId = user.Id,
                Message = "Done",
                UserName = user.GetFullName()
            },
            Message = "User Update Successful",
            StatusCode = HttpStatusCode.OK
        };
    }


}
