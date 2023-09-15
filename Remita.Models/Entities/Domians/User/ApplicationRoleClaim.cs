using Microsoft.AspNetCore.Identity;

namespace Remita.Models.Entities.Domians.User
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; } = true;
    }
}