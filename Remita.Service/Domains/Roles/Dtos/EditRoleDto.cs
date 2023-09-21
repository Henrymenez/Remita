using Remita.Models.Utility;

namespace Remita.Services.Domains.Roles.Dtos;

public record EditRoleDto : BaseRecord
{
    public string RoleId { get; set; } = null!;
    public string name { get; set; } = null!;
}
