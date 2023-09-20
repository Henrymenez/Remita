using Remita.Models.Entities.Domians.User;
using Remita.Services.Domains.Security;

namespace Remita.Services.Domains.OutboundNotifications;

public class NotificationManagerService : INotificationManagerService
{
    private readonly IOtpCodeService _otpCodeService;

    private TimeSpan OtpTtl = TimeSpan.FromMinutes(5);
    public NotificationManagerService(IOtpCodeService otpCodeService)
    {
        _otpCodeService = otpCodeService;
    }
    public Task CreateAccountLockOutNotification(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Task CreateEmailConfirmedNotification(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Task CreatePasswordResetNotification(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Task CreateResetPasswordNotification(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Task CreateSignInNotification(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Task CreateSignUPNotification(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Task CreateVerifyEmailNotification(ApplicationUser user)
    {
        throw new NotImplementedException();
    }
}
