using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;
using Remita.Services.Domains.Admin;
using Remita.Services.Domains.Admin.Dtos;
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

    [AllowAnonymous]
    [HttpPost("user-creation", Name = "user-creation")]
    [SwaggerOperation(Summary = "Admin creates user")]
   /* [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of created user", Type = typeof(AuthenticationResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User with provided email already exists", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]*/
    public async Task<IActionResult> CreateUser(AdminUserRegistrationDto request)
    {
        var response = await _adminService.CreateNewUser(request);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPut("user-update", Name = "user-update")]
    [SwaggerOperation(Summary = "Updates user")]
  /*  [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of updated user", Type = typeof(AccountResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to update user", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]*/

    public async Task<IActionResult> UpdateUser(string email, [FromBody] UpdateUserDto request)
    {
        var response = await _adminService.UpdateUser(email, request);
        return Ok(response);
    }


    [AllowAnonymous]
    [HttpDelete("delete-user", Name = "delete-user")]
    [SwaggerOperation(Summary = "Deletes user")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "User deleted")]
   /* [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User not found", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]*/
    public async Task<IActionResult> DeleteUser(string email)
    {
        await _adminService.DeleteUser(email);
        return Ok();
    }

}
