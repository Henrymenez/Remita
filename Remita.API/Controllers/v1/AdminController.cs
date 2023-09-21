using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;
using Remita.Models.Utility;
using Remita.Services.Domains.Admin;
using Remita.Services.Domains.Admin.Dtos;
using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Domains.User.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Remita.Api.Controllers.v1;


[Route("api/v{version:apiVersion}/admin")]
[ApiController]
public class AdminController : BaseController
{
    private readonly IAdminService _adminService;
    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("user-creation", Name = "user-creation")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<AccountResponse>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> CreateUser(AdminUserRegistrationDto request)
    {
        var response = await _adminService.CreateNewUser(request);
        return Ok(response);
    }

    [HttpPut("user-update", Name = "user-update")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<AccountResponse>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]

    public async Task<IActionResult> UpdateUser(string email, [FromBody] UpdateUserDto request)
    {
        var response = await _adminService.UpdateUser(email, request);
        return Ok(response);
    }

    [HttpPut("activate-user", Name = "user-activate")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]

    public async Task<IActionResult> ActivateUser(string email)
    {
        var response = await _adminService.ActivateUser(email);
        return ComputeResponse(response);
    }


    [HttpDelete("delete-user", Name = "delete-user")]
    [SwaggerOperation(Summary = "Deletes user")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> DeleteUser(string email)
    {
        var result = await _adminService.DeleteUser(email);
        return ComputeResponse(result);
    }

}
