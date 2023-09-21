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
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<AccountResponse>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> CreateRole(RoleDto request)
    {
        var response = await _roleService.CreateRole(request);
        return Ok(response);
    }

    [HttpPut("Edit-Role", Name = "Edit-Role")]
    [SwaggerOperation(Summary = "Edits An Existing Role")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<AccountResponse>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<ActionResult> EditRole(string id, string name)
    {
        var response = await _roleService.EditRole(id, name);
        return Ok(response);
    }

    [HttpDelete("Delete-Role", Name = "Delete-Existing-Role")]
    [SwaggerOperation(Summary = "Deletes An Existing Role")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<AccountResponse>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> DeleteRole(string name)
    {
        var response = await _roleService.DeleteRole(name);
        return Ok(response);
    }


    [HttpPut("Add-User-To-Role", Name = "Add-User-To-Role")]
    [SwaggerOperation(Summary = "Add User To Role")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<AccountResponse>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> AddUserToRole(string userId, string roleName)
    {
        var result = await _roleService.AddUserToRole(userId, roleName);
        return Ok(result);

    }
    [HttpGet("Get-All-Roles", Name = "Get-All-Roles")]
    [SwaggerOperation(Summary = "Get All Roles")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<AccountResponse>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await _roleService.GetAllRoles();
        return Ok(result);
    }

}
