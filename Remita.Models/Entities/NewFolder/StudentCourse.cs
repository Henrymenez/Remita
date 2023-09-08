using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTS.Models.Entities
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public int ExamScore{ get; set; }
        public int CaScore { get; set; }

        public int level { get; set; }

        public ApplicationUser Student { get; set; }
        public Course Course { get; set; }
    }
}
