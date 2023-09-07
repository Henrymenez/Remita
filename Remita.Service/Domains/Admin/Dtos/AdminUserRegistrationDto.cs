using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Admin.Dtos;

public record AdminUserRegistrationDto
{
    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Username { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Phone]
    public string Phone { get; set; } = null!;
}
