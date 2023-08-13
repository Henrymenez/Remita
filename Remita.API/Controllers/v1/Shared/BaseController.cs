using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Remita.Models.Entities.Domians.User;
using Remita.Models.Utility;
using Remita.Services.Utility;
using System.Net;
using System.Security.Claims;

namespace Remita.Controllers.v1.Shared;

[Produces("application/json")]
public class BaseController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    public BaseController()
    {

    }

    public BaseController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ParseResult(ServiceResult response)
    {
        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            return base.Ok(response);
        }
        else if (response.HttpStatusCode == HttpStatusCode.BadRequest)
        {
            return base.BadRequest(response);
        }
        else
        {
            throw new InvalidOperationException("Unsupported Result Status");
        }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        return await _userManager.GetUserAsync(HttpContext.User);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<string> GetUserIdAsync()
    {
        var user = await GetCurrentUserAsync();

        return user.Id;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public string GetUserId()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? HttpContext.User.FindFirstValue("UserId");

        return userId;
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ComputeApiResponse<T>(ServiceResponse<T> serviceResponse) where T : BaseRecord
    {

        switch (serviceResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                var response = new ApiRecordResponse<T>(true, serviceResponse.Message, serviceResponse.Data);
                return Ok(response);

            case HttpStatusCode.Unauthorized:
                response = new ApiRecordResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return Unauthorized(response);

            case HttpStatusCode.NotFound:
                response = new ApiRecordResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return NotFound(response);

            case HttpStatusCode.BadRequest:
                response = new ApiRecordResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return BadRequest(response);
            default:
                throw new ArgumentOutOfRangeException("HTTP Status Code Could Not Be Deciphered", nameof(serviceResponse.StatusCode));

        }
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ComputeResponse<T>(ServiceResponse<T> serviceResponse) where T : class
    {
        switch (serviceResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                var response = new ApiResponse<T>(true, serviceResponse.Message, serviceResponse.Data);
                return Ok(response);

            case HttpStatusCode.Unauthorized:
                response = new ApiResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return Unauthorized(response);

            case HttpStatusCode.NotFound:
                response = new ApiResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return NotFound(response);

            case HttpStatusCode.BadRequest:
                response = new ApiResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return BadRequest(response);
            default:
                throw new ArgumentOutOfRangeException("HTTP Status Code Could Not Be Deciphered", nameof(serviceResponse.StatusCode));

        }
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ComputeResponse(ServiceResponse serviceResponse)
    {
        switch (serviceResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                var response = new ApiResponse(true, serviceResponse.Message);
                return Ok(response);

            case HttpStatusCode.Unauthorized:
                response = new ApiResponse(false, serviceResponse.Message);
                return Unauthorized(response);

            case HttpStatusCode.NotFound:
                response = new ApiResponse(false, serviceResponse.Message);
                return NotFound(response);

            case HttpStatusCode.BadRequest:
                response = new ApiResponse(false, serviceResponse.Message);
                return BadRequest(response);
            default:
                throw new ArgumentOutOfRangeException("HTTP Status Code Could Not Be Deciphered", nameof(serviceResponse.StatusCode));

        }
    }

}
