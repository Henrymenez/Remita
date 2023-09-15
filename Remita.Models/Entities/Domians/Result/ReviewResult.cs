using Remita.Models.Domains.ApplicationFee.Enum;
using Remita.Models.Entities.Domians.User;

namespace Remita.Models.Entities.Domians.Result;

public class ReviewResult : BaseEntity
{
    public ApprovalStatus ApprovalStatus { get; set; }
    public DateTime ActionDate { get; set; } = DateTime.Now;
    public string ReviewerId { get; set; } = null!;
    public ApplicationUser Reviewer { get; set; } = null!;
    public Guid ResultId { get; set; }
    public Result Result { get; set; } = null!;
}
