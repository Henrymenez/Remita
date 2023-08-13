using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;


[Route("api/v{version:apiVersion}/users")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class UserController : BaseController
{
    /*private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UserController(IUserService userService,
        IAuthService authService,
        UserManager<ApplicationUser> userManager) : base(userManager)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpGet("profile")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<ProfileDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> GetProfile()
    {
        string userId = GetUserId();

        ServiceResponse<ProfileDto> response = await _userService.GetUserProfileAsync(userId);

        return ComputeApiResponse(response);
    }

    [HttpGet("achievements")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<PaginationResponse<AchievementsDto>>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> GetAchievements([FromQuery] int? page = 1, [FromQuery] int? pageSize = 30)
    {
        ServiceResponse<PaginationResponse<AchievementsDto>> response = await _userService.GetUserAchievementsAsync(GetUserId(), page.Value, pageSize.Value);

        return ComputeApiResponse(response);
    }


    [HttpPut("update")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> UpdateUserBio([FromForm] UpdateBioDto model)
    {
        ServiceResponse response = await _userService.UpdateUserProfileAsync(GetUserId(), model);

        return ComputeResponse(response);
    }


    [HttpGet("following/{followingType}")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<PaginationResponse<FollowingsDto>>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> GetUserFollow([FromRoute] FollowingType followingType, [FromQuery] string? searchParam, [FromQuery] int? page = 1, [FromQuery] int? pageSize = 30)
    {
        ServiceResponse<PaginationResponse<FollowingsDto>> response = await _userService.GetUserFollowsAsync(GetUserId(), followingType, searchParam, page.Value, pageSize.Value);

        return ComputeApiResponse(response);
    }

    [AllowAnonymous]
    [HttpGet("profile/user/{userId}")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<PublicUserProfileDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> GetPublicUserProfile([FromRoute] string userId)
    {
        ServiceResponse<PublicUserProfileDto> response = await _userService.GetUserPublicProfileAsync(userId);

        return ComputeApiResponse(response);
    }

    [AllowAnonymous]
    [HttpGet("achievements/user/{userId}")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<PaginationResponse<AchievementsDto>>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> GetPublicUserAchievements([FromRoute] string userId, [FromQuery] int? page = 1, [FromQuery] int? pageSize = 20)
    {
        ServiceResponse<PaginationResponse<AchievementsDto>> response = await _userService.GetUserAchievementsAsync(userId, page.Value, pageSize.Value);

        return ComputeApiResponse(response);
    }

    [HttpPost("change-email")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> ChangeEmail([FromBody] ConfirmEmailDto model)
    {
        var Result = await _authService.ChangeEmailAsync(GetUserId(), model);

        return ComputeResponse(Result);
    }

    [HttpPost("change-password")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
    {
        var Result = await _authService.ChangePasswordAsync(GetUserId(), model);

        return ComputeResponse(Result);
    }
*/
}

