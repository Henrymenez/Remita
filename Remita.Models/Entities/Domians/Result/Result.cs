using Remita.Models.Domains.ApplicationFee.Enum;
using Remita.Models.Entities.Domians.User;

namespace Remita.Models.Entities.Domians.Result;

public class Result : BaseEntity
{
    public string RegistrationNumber { get; set; } = null!;
    public string StudentName { get; set; } = null!;
    public string Program { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public int CourseCredit { get; set; }
    public string CourseCode { get; set; } = null!;
    public double Score { get; set; }
    public string Grade { get; set; } = null!;
    public int GradePoint { get; set; }
    public string Level { get; set; } = null!;
    public string AcademicYear { get; set; } = null!;
    public string Semester { get; set; } = null!;
    public DateTime UploadedAt { get; set; } = DateTime.Now;
    public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;
    public Guid UploaderId { get; set; }
    public virtual  ApplicationUser? Uploader { get; set; }
}
