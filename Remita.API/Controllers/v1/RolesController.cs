using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;
using Remita.Models.Utility;
using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Domains.Roles;
using Remita.Services.Domains.Roles.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Remita.Api.Controllers.v1;

[Route("api/v{version:apiVersion}/roles")]
[ApiVersion("1.0")]
[ApiController]
public class RolesController : BaseController
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }


    [HttpPost("Create-Role", Name = "Create-New-Role")]
    [SwaggerOperation(Summary = "Creates user")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<RoleResponseDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> CreateRole(RoleDto request)
    {
        var response = await _roleService.CreateRole(request);
        return ComputeApiResponse(response);
    }

    [HttpPut("Edit-Role", Name = "Edit-Role")]
    [SwaggerOperation(Summary = "Edits An Existing Role")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<RoleResponseDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> EditRole(EditRoleDto editRoleDto)
    {
        var response = await _roleService.EditRole(editRoleDto);
        return ComputeApiResponse(response);
    }

    [HttpDelete("Delete-Role", Name = "Delete-Existing-Role")]   
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<RoleResponseDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> DeleteRole(string name)
    {
        var response = await _roleService.DeleteRole(name);
        return ComputeApiResponse(response);
    }


    [HttpPut("Add-User-To-Role", Name = "Add-User-To-Role")]
    [SwaggerOperation(Summary = "Add User To Role")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<RoleResponseDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> AddUserToRole(AddRoleDto addRoleDto)
    {
        var result = await _roleService.AddUserToRole(addRoleDto);
        return ComputeApiResponse(result);

    }
    [HttpGet("Get-All-Roles", Name = "Get-All-Roles")]
    [SwaggerOperation(Summary = "Get All Roles")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await _roleService.GetAllRoles();
        return ComputeResponse(result);
    }

}
