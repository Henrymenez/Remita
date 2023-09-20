using Remita.Models.Entities.Domians.User;

namespace Remita.Services.Domains.OutboundNotifications;

public interface INotificationManagerService
{

    /// <summary>
    /// Send out SignUp Email notification to the queue
    /// </summary>
    /// <param name="user"></param>
    /// <param name="activity"></param>
    /// <returns></returns>
    Task CreateSignUPNotification(ApplicationUser user);

    /// <summary>
    /// Send out account lockout Email notification to the queue
    /// </summary>
    /// <param name="user"></param>
    /// <param name="activity"></param>
    /// <returns></returns>
    Task CreateAccountLockOutNotification(ApplicationUser user);

    /// <summary>
    /// Sends out Sign in activity email to message queue
    /// </summary>
    /// <param name="user"></param>
    /// <param name="activity"></param>
    /// <returns></returns>
    Task CreateSignInNotification(ApplicationUser user);

    /// <summary>
    /// Sends out verify email otp to message queue
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task CreateVerifyEmailNotification(ApplicationUser user);

    /// <summary>
    /// Send out email confirmed Email notification to the queue
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task CreateEmailConfirmedNotification(ApplicationUser user);

    /// <summary>
    /// Send out successful password reset Email notification to the queue
    /// </summary>
    /// <param name="user"></param>
    /// <param name="activity"></param>
    /// <returns></returns>
    Task CreatePasswordResetNotification(ApplicationUser user);

    /// <summary>
    /// Send out reset password otp Email notification to the queue
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task CreateResetPasswordNotification(ApplicationUser user);

}
