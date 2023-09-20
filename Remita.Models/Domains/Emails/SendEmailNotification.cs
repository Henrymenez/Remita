using Remita.Models.Domains.Emails.Enums;

namespace Remita.Models.Domains.Emails;

public class SendEmailNotification
{
    public EmailCategory Category { get; init; }
    public string Subject { get; init; } = null!;
    public Personality To { get; init; } = null!;
    public bool IsTransactional { get; init; }
    public TimeSpan? TTL { get; init; }
    public DateTime CommandSentAt { get; init; }
    public List<Personality> CCs { get; init; } = new List<Personality>();
    public List<Personality> BCCs { get; init; } = new List<Personality>();
    public string[]? Contents { get; init; }
    public string? Source { get; init; }
    public string MessageId { get; init; } = null!;
}
