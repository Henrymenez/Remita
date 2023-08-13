namespace Remita.Models.Configuration.ApiConfiguration;
public class QueueConfiguration
{
    public string NotificationQueueUrl { get; set; } = null!;
    public int TimeOutInSeconds { get; set; }
    public int VisibilityInSeconds { get; set; }
}
