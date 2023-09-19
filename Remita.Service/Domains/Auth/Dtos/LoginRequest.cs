using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Auth.Dtos;

public record LoginRequest
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
