using Remita.Models.Domains.Emails.Enums;

namespace Remita.Services.Domains.OutboundNotifications.Dtos;

internal record NonTransactionalEmailNotificationDto(
EmailCategory Category,
string Subject,
string MessageId,
string Email,
string FullName,
IList<string>? Content);
