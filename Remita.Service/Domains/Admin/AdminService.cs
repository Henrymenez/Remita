using AutoMapper;
using Remita.Data.Interfaces;
using Remita.Models.DatabaseContexts;
using Remita.Services.Domains.Admin.Dtos;
using Remita.Services.Domains.User.Dtos;
using Remita.Services.Utility;

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

    public Task<bool> DeleteUser(string email)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<UserResponse>> UpdateUser(string email, UpdateUserDto request)
    {
        throw new NotImplementedException();
    }

   
}
