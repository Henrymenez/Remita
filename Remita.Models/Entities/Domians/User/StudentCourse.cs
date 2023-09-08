namespace Remita.Models.Entities.Domians.User;

public class StudentCourse : BaseEntity
{
    public int ExamScore { get; set; }
    public int CaScore { get; set; }
    public int level { get; set; }
    public ApplicationUser Student { get; set; }
    public Course Course { get; set; }
}
