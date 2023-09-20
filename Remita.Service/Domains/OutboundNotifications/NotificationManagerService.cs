using Remita.Models.Domains.Emails;
using Remita.Models.Domains.Emails.Enums;
using Remita.Models.Domains.Security;
using Remita.Models.Entities.Domians.User;
using Remita.Services.Domains.Activities.Dtos;
using Remita.Services.Domains.OutboundNotifications.Dtos;
using Remita.Services.Domains.Security;
using Remita.Services.Domains.User;
using Remita.Services.Utility;

namespace Remita.Services.Domains.OutboundNotifications;

public class NotificationManagerService : INotificationManagerService
{
    private readonly IOtpCodeService _otpCodeService;

    private TimeSpan OtpTtl = TimeSpan.FromMinutes(5);
    public NotificationManagerService(IOtpCodeService otpCodeService)
    {
        _otpCodeService = otpCodeService;
    }
    public async Task CreateAccountLockOutNotification(ApplicationUser user)
    {
        var messageId = MessageIdGenerator.GenerateMessageId<SignUpActivity>(user.Id, DateTime.UtcNow.Date.ToShortDateString());
        var contents = new List<string>() { user.UserName! };
        var lockoutCommand = new NonTransactionalEmailNotificationDto(EmailCategory.LockOut, Constants.LockOutEmailSubject, messageId, user.Email!, user.GetFullName(), contents);
        await SendNonTransactionalEmailNotification(lockoutCommand, CancellationToken.None);
    }

    public async  Task CreateEmailConfirmedNotification(ApplicationUser user)
    {
        var messageId = MessageIdGenerator.GenerateMessageId<SignUpActivity>(user.Id, DateTime.UtcNow.Date.ToShortDateString());
        var command = new NonTransactionalEmailNotificationDto(EmailCategory.EmailConfirmation, Constants.ConfirmationEmailSubject, messageId, user.Email!, user.GetFullName(), null);
        await SendNonTransactionalEmailNotification(command, CancellationToken.None);
    }

    public async Task CreatePasswordResetNotification(ApplicationUser user)
    {
        var messageId = MessageIdGenerator.GenerateMessageId<SignUpActivity>(user.Id, DateTime.UtcNow.Date.ToShortDateString());
        var contents = new List<string>() { user.UserName! };
        var command = new NonTransactionalEmailNotificationDto(EmailCategory.PasswordReset, Constants.PasswordResetEmailSubject, messageId, user.Email!, user.GetFullName(), contents);
        await SendNonTransactionalEmailNotification(command, CancellationToken.None);
    }

    public async Task CreateResetPasswordNotification(ApplicationUser user)
    {
        var otp = await _otpCodeService.GenerateOtpAsync(user.Id, OtpOperation.PasswordReset);
        var messageId = MessageIdGenerator.GenerateMessageId<SignUpActivity>(user.Id, DateTime.UtcNow.Date.ToShortDateString(), "OTP");
        var contents = new List<string>() { user.UserName!, otp };
        var command = new TransactionalEmailNotificationDto(EmailCategory.ResetPasswordOTP, Constants.ForgotPasswordEmailSubject, messageId, user.Email!, user.UserName!, OtpTtl, contents);
        await SendTransactionalEmailNotification(command, CancellationToken.None);
        throw new NotImplementedException();
    }

    public async Task CreateSignInNotification(ApplicationUser user)
    {
        var messageId = MessageIdGenerator.GenerateMessageId<SignUpActivity>(user.Id, DateTime.UtcNow.Date.ToShortDateString());
        var contents = new List<string>()
        {
            user.UserName!,
            DateTime.Now.ToShortDateString()
        };
        var command = new NonTransactionalEmailNotificationDto(EmailCategory.SignInActivity, Constants.SignInEmailSubject, messageId, user.Email!, user.UserName!, contents);
        await SendNonTransactionalEmailNotification(command, CancellationToken.None);
    }

    public async Task CreateSignUPNotification(ApplicationUser user)
    {
        var messageId = MessageIdGenerator.GenerateMessageId<SignUpActivity>(user.Id, DateTime.UtcNow.Date.ToShortDateString());
        var contents = new List<string>() { user.Email! };
        var command = new NonTransactionalEmailNotificationDto(EmailCategory.SignUp, Constants.SignUpEmailSubject, messageId, user.Email!, user.GetFullName(), contents);
        // send mail here 
        await SendNonTransactionalEmailNotification(command, CancellationToken.None);
    }

    public async Task CreateVerifyEmailNotification(ApplicationUser user)
    {
        var otp = await _otpCodeService.GenerateOtpAsync(user.Id, OtpOperation.EmailConfirmation);
        var messageId = MessageIdGenerator.GenerateMessageId<SignUpActivity>(user.Id, DateTime.UtcNow.Date.ToShortDateString(), "OTP");
        var contents = new List<string>() { user.Email!, otp };
        var command = new TransactionalEmailNotificationDto(EmailCategory.EmailConfirmationOTP, Constants.SignUpEmailSubject, messageId, user.Email!, user.GetFullName(), OtpTtl, contents);
        await SendTransactionalEmailNotification(command, CancellationToken.None);
        throw new NotImplementedException();
    }

    private async Task SendNonTransactionalEmailNotification(NonTransactionalEmailNotificationDto model, CancellationToken cancellationToken)
    {
        SendEmailNotification command = new()
        {
            Contents = model.Content?.ToArray(),
            IsTransactional = false,
            Subject = model.Subject,
            Category = model.Category,
            MessageId = model.MessageId,
            Source = MessagingSource.API,
            CommandSentAt = DateTime.UtcNow,
            To = new Personality(model.Email, model.FullName),
        };
        //send mail
        // await _messageSender.SendAsync(command, cancellationToken);
        await Task.CompletedTask;
    }


    private async Task SendTransactionalEmailNotification(TransactionalEmailNotificationDto model, CancellationToken cancellationToken)
    {
        SendEmailNotification command = new()
        {
            TTL = model.TTL,
            Contents = model.Content?.ToArray(),
            IsTransactional = true,
            Subject = model.Subject,
            Category = model.Category,
            MessageId = model.MessageId,
            Source = MessagingSource.API,
            CommandSentAt = DateTime.UtcNow,
            To = new Personality(model.Email, model.FullName),
        };
        // send email
        //await _messageSender.SendAsync(command, cancellationToken);
        await Task.CompletedTask;
    }
}
