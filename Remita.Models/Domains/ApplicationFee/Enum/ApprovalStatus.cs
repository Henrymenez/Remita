namespace Remita.Models.Domains.ApplicationFee.Enum;

public enum ApprovalStatus
{
    Approved = 1,
    Rejected,
    Pending
}
public static class ApprovalStatusExtension
{
    public static string? GetStringValue(this ApprovalStatus approvalStatus)
    {
        return approvalStatus switch
        {
            ApprovalStatus.Approved => "approved",
            ApprovalStatus.Rejected => "rejected",
            ApprovalStatus.Pending => "pending",
            _ => null
        };
    }
}