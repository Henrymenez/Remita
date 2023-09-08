using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Auth.Dtos;

public record UserRegistrationRequest
{
    [Required]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string MobileNumber { get; set; }
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Firstname { get; set; }

    [Required]
    public string LastName { get; set; }

    public string? Department { get; set; }

    public string? MatricNumber { get; set; }
}
