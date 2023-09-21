using Remita.Services.Domains.Roles.Dtos;
using Remita.Services.Utility;

namespace Remita.Services.Domains.Roles;

public interface IRoleService
{
    public Task<ServiceResponse<RoleResponseDto>> AddUserToRole(AddRoleDto addRoleDto);
    public Task<ServiceResponse<RoleResponseDto>> CreateRole(RoleDto request);
    public Task<ServiceResponse<RoleResponseDto>> DeleteRole(string name);
    public Task<ServiceResponse<RoleResponseDto>> EditRole(EditRoleDto editRoleDto);
    public Task<ServiceResponse<IEnumerable<string>>> GetAllRoles();
}
