using Microsoft.AspNetCore.Identity;
using RMTS.Models.Enums;

namespace RMTS.Models.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string role) : base(role)
        {

        }

        public ApplicationRole()
        {

        }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; } = true;
        public UserType Type { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
        
    }
}

