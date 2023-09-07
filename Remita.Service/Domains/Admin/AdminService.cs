using Remita.Services.Domains.Admin.Dtos;
using Remita.Services.Domains.User.Dtos;

namespace Remita.Services.Domains.Admin;

public class AdminService : IAdminService
{
	public AdminService()
	{

	}

    public Task<UserResponse> CreateNewUser(AdminUserRegistrationDto request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse> UpdateUser(string email, UpdateUserDto request)
    {
        throw new NotImplementedException();
    }
}
