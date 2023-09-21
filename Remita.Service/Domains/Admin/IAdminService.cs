using Remita.Services.Domains.Admin.Dtos;
using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Domains.User.Dtos;
using Remita.Services.Utility;

namespace Remita.Services.Domains.Admin;

public interface IAdminService
{
    Task<ServiceResponse<UserResponse>> UpdateUser(string email, UpdateUserDto request);
    Task<ServiceResponse<AccountResponse>> CreateNewUser(AdminUserRegistrationDto request);
    Task<ServiceResponse> DeleteUser(string email);
    Task<ServiceResponse> ActivateUser(string email);
}
