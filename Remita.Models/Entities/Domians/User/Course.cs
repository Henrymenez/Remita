namespace Remita.Models.Entities.Domians.User;

public class Course : BaseEntity
{

    public string Code { get; set; }
    public string Title { get; set; }
    public int Unit { get; set; }
    public ApplicationUser Teacher { get; set; }
}
