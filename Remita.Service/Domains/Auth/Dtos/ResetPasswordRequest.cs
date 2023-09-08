using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Auth.Dtos;

public record ResetPasswordRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string AuthenticationToken { get; set; }
    [Required]
    public string NewPassword { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }
}
