using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;
using Remita.Models.Utility;
using Remita.Services.Domains.Auth;
using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Utility;
using System.Net;

[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("sign-up", Name = "sign-up")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<AccountResponse>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> CreateUser([FromBody] UserRegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            var response = new ServiceResponse<AccountResponse>()
            {
                Message = errors.FirstOrDefault(),
                StatusCode = HttpStatusCode.BadRequest
            };
            return ComputeApiResponse(response);
        }

        var result = await _authService.CreateUser(request);
        return ComputeApiResponse(result);
    }

    [HttpPost("sign-in", Name = "sign-in")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<AuthenticationResponse>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            var response = new ServiceResponse<AuthenticationResponse>()
            {
                Message = errors.FirstOrDefault(),
                StatusCode = HttpStatusCode.BadRequest
            };
            return ComputeApiResponse(response);
        }

        var result = await _authService.UserLogin(request);
        return ComputeApiResponse(result);
    }



    [HttpPost("password-reset-otp", Name = "password-reset-otp")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            var response = new ServiceResponse()
            {
                Message = errors.FirstOrDefault(),
                StatusCode = HttpStatusCode.BadRequest
            };
            return ComputeResponse(response);
        }
        var Result = await _authService.ForgotPasswordAsync(email);
        return ComputeResponse(Result);
    }


    [HttpPost("password-reset",Name = "password-reset")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            var response = new ServiceResponse()
            {
                Message = errors.FirstOrDefault(),
                StatusCode = HttpStatusCode.BadRequest
            };
            return ComputeResponse(response);
        }
        var Result = await _authService.ResetPasswordAsync(request);
        return ComputeResponse(Result);
    }

    [HttpPost("verify-email-otp",Name = "verify-email-otp")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> VerifyEmailOtp([FromBody] ConfirmationEmailOtpDto model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            var response = new ServiceResponse()
            {
                Message = errors.FirstOrDefault(),
                StatusCode = HttpStatusCode.BadRequest
            };
            return ComputeResponse(response);
        }
        ServiceResponse Result = await _authService.SendEmailConfirmationOtpAsync(model);
        return ComputeResponse(Result);
    }

    [HttpPost("verify-email",Name = "verify-email")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> VerifyEmail([FromBody] ConfirmEmailDto model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            var response = new ServiceResponse()
            {
                Message = errors.FirstOrDefault(),
                StatusCode = HttpStatusCode.BadRequest
            };
            return ComputeResponse(response);
        }
        var Result = await _authService.ConfirmEmailAsync(model);
        return ComputeResponse(Result);
    }

    [HttpPost("refresh",Name = "refresh")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> Refresh(string accessToken, string refreshToken)
    {
        var tokenResponse = await _authService.RefreshAccessTokenAsync(accessToken, refreshToken);
        return ComputeResponse(tokenResponse);
    }

  
}
