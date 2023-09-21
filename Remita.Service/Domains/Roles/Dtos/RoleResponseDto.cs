using Remita.Models.Utility;

namespace Remita.Services.Domains.Roles.Dtos;

public record RoleResponseDto : BaseRecord
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int ClaimCount { get; set; }
    public bool Active { get; set; }
}
