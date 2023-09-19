using Microsoft.AspNetCore.Identity;
using Remita.Models.Domains.User.Enums;

namespace Remita.Models.Entities.Domians.User
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole(string role) : base(role)
        {

        }

        public string UserId { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; } = true;
        public UserType Type { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }

    }
}

