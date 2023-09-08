using RMTS.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace RMTS.Models.Entities
{
    public class Result
    {
        [Key]
        public Guid ID { get; set; }
        public string RegistrationNumber { get; set; }
        public string StudentName { get; set; }
        public string Program { get; set; }
        public string CourseName { get; set; }
        public int CourseCredit { get; set; }
        public string CourseCode { get; set; }
        public double Score { get; set; }
        public string Grade { get; set; }
        public int GradePoint { get; set; }
        public string Level { get; set; }
        public string AcademicYear { get; set; }
        public string Semester { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;
        public string? UploaderId { get; set; }
        public ApplicationUser? Uploader { get; set; }
        
    }
}
