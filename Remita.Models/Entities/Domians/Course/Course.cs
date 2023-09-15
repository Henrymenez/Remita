using Remita.Models.Entities.Domians.User;

namespace Remita.Models.Entities.Domians.Course;

public class Course : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Unit { get; set; }
    public ApplicationUser Teacher { get; set; }
}
