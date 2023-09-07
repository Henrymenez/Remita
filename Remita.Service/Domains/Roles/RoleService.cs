using Remita.Services.Domains.Roles.Dtos;
using Remita.Services.Utility;

namespace Remita.Services.Domains.Roles;

public class RoleService : IRoleService
{
    public RoleService()
    {

    }
    public Task<ServiceResponse<RoleResponseDto>> AddUserToRole(string userId, string roleName)
    {
        throw new NotImplementedException();
    }

    public Task<RoleResponseDto> CreateRole(RoleDto request)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<RoleResponseDto>> DeleteRole(string name)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<RoleResponseDto>> EditRole(string id, string name)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<IEnumerable<string>>> GetAllRoles()
    {
        throw new NotImplementedException();
    }
}
