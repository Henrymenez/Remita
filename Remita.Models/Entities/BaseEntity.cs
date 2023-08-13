using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTS.Models.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; } 
        public DateTime CreatedAt { get; init; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } 
    }
}
