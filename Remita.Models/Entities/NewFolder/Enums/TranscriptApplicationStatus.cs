

namespace RMTS.Models.Enums
{
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

   /* public static class StatusExtension
    {
        public static string? GetStringValue(this TranscriptApplicationStatus status)
        {
            return status switch
            {
                TranscriptApplicationStatus.Applied => "Applied",
                TranscriptApplicationStatus.Received => "Received",
                TranscriptApplicationStatus.Treated => "Treated",
                TranscriptApplicationStatus.Rejected => "Rejected",
                _ => null
            };
        }
    }*/
}
