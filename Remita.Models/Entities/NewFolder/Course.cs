using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTS.Models.Entities
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Unit { get; set; }
        public ApplicationUser Teacher { get; set; }
    }
}
