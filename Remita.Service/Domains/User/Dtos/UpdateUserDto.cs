using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.User.Dtos;

public record UpdateUserDto
{
    [Required]
    public string Firstname { get; set; } = null!;
    [Required]
    public string MiddleName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    [Phone]
    public string MobileNumber { get; set; } = null!;

}
