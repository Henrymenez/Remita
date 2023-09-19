using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Auth.Dtos;

public record ResetPasswordRequest
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string AuthenticationToken { get; set; } = null!;
    [Required]
    public string NewPassword { get; set; } = null!;

    [Required]
    public string ConfirmPassword { get; set; } = null!;
}
