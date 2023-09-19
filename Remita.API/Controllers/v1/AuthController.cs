using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;
using Remita.Models.Utility;
using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Domains.Auth.Interface;
using Remita.Services.Utility;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
[ApiController]
public class AuthController : BaseController
{
    /* private readonly IAuthService _authService;
     public AuthController(IAuthService authService)
     {
         _authService = authService;
     }

     [HttpPost("sign-up")]
     [ProducesResponseType(200, Type = typeof(ApiRecordResponse<UserSignedInDto>))]
     [ProducesResponseType(404, Type = typeof(ApiResponse))]
     [ProducesResponseType(400, Type = typeof(ApiResponse))]
     public async Task<IActionResult> SignUp([FromBody] SignUpDto model)
     {
         if (!ModelState.IsValid)
         {
             var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
             var response = new ServiceResponse<UserSignedInDto>()
             {
                 Message = errors.FirstOrDefault(),
                 StatusCode = HttpStatusCode.BadRequest
             };
             return ComputeApiResponse(response);
         }

         var result = await _authService.SignUp(model);
         return ComputeApiResponse(result);

     }

     [HttpPost("signin-google")]
     [ProducesResponseType(200, Type = typeof(ApiRecordResponse<UserSignedInDto>))]
     [ProducesResponseType(404, Type = typeof(ApiResponse))]
     [ProducesResponseType(400, Type = typeof(ApiResponse))]
     public async Task<IActionResult> GoogleSignIn()
     {
         return Ok();
     }

     [HttpPost("sign-in")]
     [ProducesResponseType(200, Type = typeof(ApiRecordResponse<UserSignedInDto>))]
     [ProducesResponseType(404, Type = typeof(ApiResponse))]
     [ProducesResponseType(400, Type = typeof(ApiResponse))]
     public async Task<IActionResult> SignIn([FromBody] SignInDto model)
     {
         if (!ModelState.IsValid)
         {
             var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
             var response = new ServiceResponse<UserSignedInDto>()
             {
                 Message = errors.FirstOrDefault(),
                 StatusCode = HttpStatusCode.BadRequest
             };
             return ComputeApiResponse(response);
         }
         var Result = await _authService.SignIn(model);
         return ComputeApiResponse(Result);
     }

     [HttpPost("password-reset-otp")]
     [ProducesResponseType(200, Type = typeof(ApiResponse))]
     [ProducesResponseType(404, Type = typeof(ApiResponse))]
     [ProducesResponseType(400, Type = typeof(ApiResponse))]
     public async Task<IActionResult> ForgotPasswordOtp([FromBody] ForgotPasswordOtpDto model)
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
         var Result = await _authService.SendForgotPasswordOtpAsync(model, CancellationToken.None);
         return ComputeResponse(Result);
     }

     [HttpPost("password-reset")]
     [ProducesResponseType(200, Type = typeof(ApiResponse))]
     [ProducesResponseType(404, Type = typeof(ApiResponse))]
     [ProducesResponseType(400, Type = typeof(ApiResponse))]
     public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
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
         var Result = await _authService.ResetPasswordAsync(model, CancellationToken.None);
         return ComputeResponse(Result);
     }

     [HttpPost("verify-email-otp")]
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
         ServiceResponse Result = await _authService.SendEmailConfirmationOtpAsync(model, CancellationToken.None);
         return ComputeResponse(Result);
     }

     [HttpPost("verify-email")]
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
         var Result = await _authService.ConfirmEmailAsync(model, CancellationToken.None);
         return ComputeResponse(Result);
     }

     [HttpPost("refresh")]
     [ProducesResponseType(200, Type = typeof(ApiResponse))]
     [ProducesResponseType(404, Type = typeof(ApiResponse))]
     [ProducesResponseType(400, Type = typeof(ApiResponse))]
     public async Task<IActionResult> Refresh(string accessToken, string refreshToken)
     {
         var tokenResponse = await _authService.RefreshAccessTokenAsync(accessToken, refreshToken);
         return ComputeResponse(tokenResponse);
     }

     [HttpDelete("delete-account")]
     [ProducesResponseType(200, Type = typeof(ApiResponse))]
     [ProducesResponseType(404, Type = typeof(ApiResponse))]
     [ProducesResponseType(400, Type = typeof(ApiResponse))]
     public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountDto model)
     {
         var Result = await _authService.DeactivateUser(model);

         return ComputeResponse(Result);
     }*/

    private readonly IAuthenticationService _authService;
    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }


    [HttpPost("sign-up")]
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


    [AllowAnonymous]
    [HttpPost("login", Name = "Login")]
    [SwaggerOperation(Summary = "Authenticates user")]
    /* [SwaggerResponse(StatusCodes.Status200OK, Description = "returns user Id", Type = typeof(AuthenticationResponse))]
     [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
     [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]*/
    public async Task<ActionResult<AuthenticationResponse>> Login(LoginRequest request)
    {
        AuthenticationResponse response = await _authService.UserLogin(request);
        return Ok(response);
    }



    [AllowAnonymous]
    [HttpPost("forgot-password", Name = "Forgot password")]
    [SwaggerOperation(Summary = "forgot password")]
    /*  [SwaggerResponse(StatusCodes.Status200OK, Description = "forgot password", Type = typeof(AuthenticationResponse))]
      [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
      [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]*/
    public async Task<IActionResult> ForgotPassword(string email)
    {
        AccountResponse response = await _authService.ForgotPasswordAsync(email);
        return Ok(response);
    }


    [AllowAnonymous]
    [HttpPost("reset-password", Name = "Reset Password")]
    [SwaggerOperation(Summary = "forgot password")]
    /* [SwaggerResponse(StatusCodes.Status200OK, Description = "returns password reset succssfully", Type = typeof(AuthenticationResponse))]
     [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
     [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]*/
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.ResetPasswordAsync(request);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        return BadRequest("Some properties are not valid");
    }
}
