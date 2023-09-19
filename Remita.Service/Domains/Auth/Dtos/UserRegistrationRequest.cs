using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Auth.Dtos;

public record UserRegistrationRequest
{
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Phone]
    public string MobileNumber { get; set; } = null!;
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }
    [Required]
    public string LastName { get; set; } = null!;

    public string? Department { get; set; }

    public string? MatricNumber { get; set; }
}
