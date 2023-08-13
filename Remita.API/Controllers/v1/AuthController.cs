﻿using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;

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
}