﻿using Microsoft.AspNetCore.Identity;
using Remita.Models.Domains.User.Enums;

namespace Remita.Models.Entities.Domians.User
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? RecoveryMail { get; set; }
        public bool Active { get; set; }
        public string? MatricNumber { get; set; }
        public string? Department { get; set; }
        public string? RefreshToken { get; set; }
        public UserType UserType { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}

