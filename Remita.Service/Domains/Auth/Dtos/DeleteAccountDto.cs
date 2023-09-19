using Remita.Models.Utility;
using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Auth.Dtos
{
    public record DeleteAccountDto : BaseRecord
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
