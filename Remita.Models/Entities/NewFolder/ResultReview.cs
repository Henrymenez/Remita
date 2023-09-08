using RMTS.Models.Enums;

namespace RMTS.Models.Entities
{
    public class ResultReview
    {
        public Guid Id { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.Now;
        public string ReviewerId { get; set; }
        public ApplicationUser? Reviewer { get; set; }
        public Guid? ResultId { get; set; }
        public Result? Result { get; set; }
    }
}
