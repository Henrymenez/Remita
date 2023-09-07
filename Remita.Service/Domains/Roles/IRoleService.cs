using Remita.Services.Domains.Roles.Dtos;
using Remita.Services.Utility;

namespace Remita.Services.Domains.Roles;

public interface IRoleService
{
    public Task<ServiceResponse<RoleResponseDto>> AddUserToRole(string userId, string roleName);
    public Task<RoleResponseDto> CreateRole(RoleDto request);
    public Task<ServiceResponse<RoleResponseDto>> DeleteRole(string name);
    public Task<ServiceResponse<RoleResponseDto>> EditRole(string id, string name);
    public Task<ServiceResponse<IEnumerable<string>>> GetAllRoles();
}
