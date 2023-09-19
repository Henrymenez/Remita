namespace Remita.Models.Domains.Course.Enum;

public enum TranscriptApplicationStatus : int
{
    Inprogress = 1,
    Approved,
    Rejected
}

public static class ApplicationStatusExtension
{
    public static string? GetStringValue(this TranscriptApplicationStatus transcriptApplicationStatus)
    {
        return transcriptApplicationStatus switch
        {
            TranscriptApplicationStatus.Approved => "approved",
            TranscriptApplicationStatus.Inprogress => "In-Progress",
            TranscriptApplicationStatus.Rejected => "rejected",
            _ => null
        };
    }
}


