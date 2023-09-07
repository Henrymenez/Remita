using Remita.Services.Domains.Admin.Dtos;
using Remita.Services.Domains.User.Dtos;

namespace Remita.Services.Domains.Admin;

public interface IAdminService
{
    Task<UserResponse> UpdateUser(string email, UpdateUserDto request);
    Task<UserResponse> CreateNewUser(AdminUserRegistrationDto request);
    Task<bool> DeleteUser(string email);
}
