namespace Remita.Models.Domains.Emails.Enums;

public enum EmailCategory
{
    /// <summary>
    /// Email category for Sign up activity, this match the template name 
    /// </summary>
    SignUp = 1,

    /// <summary>
    /// Email category for Sign in activity, this match the template name 
    /// </summary>
    SignInActivity = 2,

    /// <summary>
    /// Email category for successful verify of email, this match the template name 
    /// </summary>
    EmailConfirmationOTP = 3,

    /// <summary>
    /// Email category for successful verify of email, this match the template name 
    /// </summary>
    EmailConfirmation = 4,

    /// <summary>
    /// Email category for sending reset password otp , this match the template name 
    /// </summary> 
    ResetPasswordOTP = 5,

    /// <summary>
    /// Email category for successful reset password, this match the template name 
    /// </summary>
    PasswordReset = 6,

    /// <summary>
    /// Email category for account lockout, this match the template name 
    /// </summary>
    LockOut = 7,


}
