using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTS.Models.Entities
{
    public class UserProfile 
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string? MatricNumber { get; set; }
        public string? Department { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
