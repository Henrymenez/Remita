using Remita.Models.Domains.User.Enums;
using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.User.Dtos;

public record UpdateUserDto
{
    public string Firstname { get; set; } = null!;
    public string LastName { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Phone]
    public string MobileNumber { get; set; } = null!;

}
