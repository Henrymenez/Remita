using Remita.Models.Entities.Domians.User;

namespace Remita.Models.Entities.Domians.Course;

public class StudentCourse : BaseEntity
{
    public int ExamScore { get; set; }
    public int CaScore { get; set; }
    public int level { get; set; }
    public string StudentId { get; set; } = null!;
    public virtual ApplicationUser? Student { get; set; }
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;
}
