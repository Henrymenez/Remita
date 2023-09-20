using Remita.Models.Domains.Emails.Enums;

namespace Remita.Services.Domains.OutboundNotifications.Dtos;

internal record TransactionalEmailNotificationDto(
    EmailCategory Category,
    string Subject,
    string MessageId,
    string Email,
    string FullName,
    TimeSpan? TTL,
    List<string>? Content);

