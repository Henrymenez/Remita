using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Utility;

namespace Remita.Services.Domains.Auth;
/// <summary>
/// handles authentication related tasks
/// </summary>
public interface IAuthService
{

    //   Task<ServiceResponse<SignedInDto>> RefreshAccessTokenAsync(string accessToken, string refreshToken);
    /// <summary>
    /// Utilizes the password reset otp as a 2FA mechanism in resetting the users password
    /// </summary>
    /// <param name="model" <see cref="ResetPasswordDto"/>></param>
    /// <returns></returns>
    Task<ServiceResponse> ResetPasswordAsync(ResetPasswordRequest request);
    /// <summary>
    /// Generates a 2FA OTP for the confirmation of a users email
    /// </summary>
    /// <param name="hostelId">The unique identifier of the hostel from whose subdomain the operation is being performed</param>
    /// <param name="model" <see cref="VerifyEmailOtpDto"/>></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ServiceResponse<AuthenticationResponse>> RefreshAccessTokenAsync(string accessToken, string refreshToken);
    /// <summary>
    /// Generates a 2FA OTP as a security check when a user attempts to reset their password
    /// </summary>
    /// <param name="hostelId">The unique identifier of the hostel from whose subdomain the operation is being performed</param>
    /// <param name="model" <see cref="ForgotPasswordOtpDto"/>></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ServiceResponse> ForgotPasswordAsync(string email);
    /// <summary>
    /// Validates user login credentials and generates an accesstoken and a refresh token
    /// </summary>
    /// <param name="hostelId">The unique identifier of the hostel from whose subdomain the operation is being performed</param>
    /// <param name="model" <see cref="SignInDto"/>></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ServiceResponse<AuthenticationResponse>> UserLogin(LoginRequest request);
    /// <summary>
    /// Handles creation of new user accounts
    /// </summary>
    /// <param name="UserRegistrationRequest">The unique identifier of the hostel from whose subdomain the operation is being performed</param>
    /// <param name="model" <see cref="SignUpDto"/>></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ServiceResponse<AccountResponse>> CreateUser(UserRegistrationRequest request);
    /// <summary>
    /// Utilizes the email confirmation OTP as a 2FA mechanism in confirming the users email address
    /// </summary>
    /// <param name="model" <see cref="VerifyEmailDto"/>></param>
    /// <returns></returns>

    Task<ServiceResponse> SendEmailConfirmationOtpAsync(ConfirmationEmailOtpDto otpDto);

   Task<ServiceResponse> ConfirmEmailAsync(ConfirmEmailDto otpDto);


}