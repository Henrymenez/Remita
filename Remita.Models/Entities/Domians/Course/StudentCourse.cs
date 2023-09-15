using Remita.Models.Entities.Domians.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remita.Models.Entities.Domians.Course;

public class StudentCourse : BaseEntity
{
    public int ExamScore { get; set; }
    public int CaScore { get; set; }
    public int level { get; set; }
    public ApplicationUser Student { get; set; } = null!;
    public Course Course { get; set; } = null!;
}
