using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Roles.Dtos;

public record RoleDto
{
    [Required(ErrorMessage = "Role Name cannot be empty"), MinLength(2), MaxLength(30)]
    public string Name { get; set; } = null!;
    public string UserType { get; set; } = null!;
}
