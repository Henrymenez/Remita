using Microsoft.AspNetCore.Identity;
using Remita.Models.Domains.User.Enums;
using Remita.Models.Entities.Domians.User;
using Remita.Services.Domains.Roles.Dtos;
using Remita.Services.Domains.User;
using Remita.Services.Utility;
using System.Net;

namespace Remita.Services.Domains.Roles;

public class RoleService : IRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public async Task<ServiceResponse<RoleResponseDto>> AddUserToRole(AddRoleDto addRoleDto)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(addRoleDto.userId);

        if (user == null)
        {
            return new ServiceResponse<RoleResponseDto>()
            {
                Message = $"User '{addRoleDto.userId}' does not Exist!",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
        ApplicationRole? role = await _roleManager.FindByNameAsync(addRoleDto.roleName);

        if (role == null)
        {
            return new ServiceResponse<RoleResponseDto>()
            {
                Message = $"Role '{addRoleDto.roleName}' does not Exist!",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        user.UserType = role.Type;
        user.UpdatedAt = DateTime.Now;

        await _userManager.AddToRoleAsync(user, role.Name!);

        return new ServiceResponse<RoleResponseDto>()
        {
            Message = $"{user.GetFullName()} has been assigned a {addRoleDto.roleName} role",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ServiceResponse<RoleResponseDto>> CreateRole(RoleDto request)
    {
        ApplicationRole? role = await _roleManager.FindByNameAsync(request.Name.Trim().ToLower());

        if (role != null)
        {
            return new ServiceResponse<RoleResponseDto>()
            {
                Message = $"Role with name {request.Name} already exist",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        ApplicationRole roleToCreate = new()
        {
            Id = Guid.NewGuid().ToString(),
            Active = true,
            Name = request.Name,
            Type = request.UserType,
            CreatedAt = DateTime.Now
        };

        await _roleManager.CreateAsync(roleToCreate);

        return new ServiceResponse<RoleResponseDto>()
        {
            StatusCode = HttpStatusCode.OK,
            Data = new RoleResponseDto()
            {
                Id = roleToCreate.Id,
                Name = roleToCreate.Name,
                Active = roleToCreate.Active
            }
        };
    }

    public async Task<ServiceResponse<RoleResponseDto>> DeleteRole(string name)
    {
        ApplicationRole? role = await _roleManager.FindByNameAsync(name.Trim().ToLower());

        if (role == null)
        {
            return new ServiceResponse<RoleResponseDto>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = $" The Role '{name}' does not Exist"
            };
        }

        if (_userManager.Users.Any(x => x.UserType == role.Type))
        {
            return new ServiceResponse<RoleResponseDto>()
            {
                Message = $"Assigned roles cannot be deleted",
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        await _roleManager.DeleteAsync(role);

        return new ServiceResponse<RoleResponseDto>()
        {
            Message = $"{name} has been successfully deleted",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ServiceResponse<RoleResponseDto>> EditRole(EditRoleDto editRoleDto)
    {
        ApplicationRole? role = await _roleManager.FindByIdAsync(editRoleDto.RoleId);
        if (role == null)
        {
            return new ServiceResponse<RoleResponseDto>
            {
                Message = $"Role with {editRoleDto.RoleId} not found",
                StatusCode = HttpStatusCode.NotFound
            };

        }
        role.Name = editRoleDto.name;
        role.UpdatedAt = DateTime.Now;

        await _roleManager.UpdateAsync(role);
        return new ServiceResponse<RoleResponseDto>
        {
            Data = new RoleResponseDto
            {
                Active = role.Active,
                Name = role.Name,
                Id = role.Id
            },
            StatusCode = HttpStatusCode.OK,
            Message = "Successfully Updated"
        };
    }

    public async Task<ServiceResponse<IEnumerable<string>>> GetAllRoles()
    {
        var myRoles = _roleManager.Roles.Select(x => x.Name).ToList();

        return new ServiceResponse<IEnumerable<string>>
        {
            Data = myRoles,
            Message = "Roles",
            StatusCode = HttpStatusCode.OK,

        };
    }
}
