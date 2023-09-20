using AutoMapper;
using Remita.Data.Interfaces;
using Remita.Models.DatabaseContexts;
using Remita.Models.Entities.Domians.User;
using Remita.Services.Domains.Admin.Dtos;
using Remita.Services.Domains.User;
using Remita.Services.Domains.User.Dtos;
using Remita.Services.Utility;
using System.Net;

namespace Remita.Services.Domains.Admin;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
    private readonly IMapper _mapper;
    public AdminService(IUnitOfWork<ApplicationDbContext> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<ServiceResponse<UserResponse>> CreateNewUser(AdminUserRegistrationDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse> DeleteUser(string email)
    {
        var user = await _unitOfWork.GetRepository<ApplicationUser>().SingleAsync(u => u.Email == email);
        if (user == null)
        {
            return new ServiceResponse()
            {
                Message = "User Not Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }
        _unitOfWork.GetRepository<ApplicationUser>().Delete(user);
        await _unitOfWork.SaveChangesAsync();
        return new ServiceResponse()
        {
            Message = "User Account Deleted",
            StatusCode = HttpStatusCode.NoContent
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
