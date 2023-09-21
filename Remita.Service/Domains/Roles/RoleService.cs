﻿using Microsoft.AspNetCore.Identity;
using Remita.Models.Domains.User.Enums;
using Remita.Models.Entities.Domians.User;
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
        ApplicationUser user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new InvalidOperationException($"User '{userId}' does not Exist!");
        }
        ApplicationRole role = await _roleManager.FindByNameAsync(roleName);

        if (role is null)
        {
            throw new InvalidOperationException($"Role '{roleName}' does not Exist!");
        }

        user.UserTypeId = role.Type;
        user.UpdatedAt = DateTime.Now;

        await _userManager.AddToRoleAsync(user, role.Name);

        return new Response<RoleResponse>()
        {
            Message = $"{userId} has been assigned a {roleName} role",
            IsSuccessful = true
        };
    }

    public Task<RoleResponseDto> CreateRole(RoleDto request)
    {
        ApplicationRole role = await _roleManager.FindByNameAsync(request.Name.Trim().ToLower());

        if (role is not null)
        {
            throw new InvalidOperationException($"Role with name {request.Name} already exist");
        }


        if (Enum.IsDefined(typeof(UserType), request.UserType))
        {
            throw new InvalidOperationException($"Role with UserType {request.UserType} already exist");
        }
        ApplicationRole roleToCreate = new()
        {
            Name = request.Name,
            Type = request.UserType,
            CreatedAt = DateTime.Now
        };

        await _roleManager.CreateAsync(roleToCreate);

        return new RoleResponse
        {
            Id = roleToCreate.Id,
            Name = roleToCreate.Name,
            Active = roleToCreate.Active
        };
    }

    public Task<ServiceResponse<RoleResponseDto>> DeleteRole(string name)
    {
        ApplicationRole role = await _roleManager.FindByNameAsync(name.Trim().ToLower());

        if (role is null)
        {
            throw new InvalidOperationException($" The Role '{name}' does not Exist");
        }

        if (_userManager.Users.Any(x => x.UserTypeId == role.Type))
        {
            return new Response<RoleResponse>
            {
                Message = $"Assigned roles cannot be deleted",
                IsSuccessful = false,
            };
        }

        await _roleManager.DeleteAsync(role);

        return new Response<RoleResponse>
        {
            Message = $"{name} has been successfully deleted",
            IsSuccessful = true,
        };
    }

    public Task<ServiceResponse<RoleResponseDto>> EditRole(string id, string name)
    {
        ApplicationRole role = await _roleManager.FindByIdAsync(id);
        if (role is null)
        {
            throw new InvalidOperationException($"Role with {id} not found");
        }
        role.Name = name;
        role.UpdatedAt = DateTime.Now;

        await _roleManager.UpdateAsync(role);
        return new Response<RoleResponse>
        {
            Message = $"Update Successful",
            IsSuccessful = true,
        };
    }

    public Task<ServiceResponse<IEnumerable<string>>> GetAllRoles()
    {
        var myRoles = _roleManager.Roles.Select(x => x.Name).ToList();

        return new Response<IEnumerable<string>>
        {
            Message = "Roles:",
            Result = myRoles,
            IsSuccessful = true

        };
    }
}
