using Remita.Models.Entities;
using Remita.Models.Utility;
using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Auth.Dtos;

public record ConfirmEmailDto : BaseRecord
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string OTP { get; set; } = null!;
}
