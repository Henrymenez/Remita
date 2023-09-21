using Remita.Models.Utility;

namespace Remita.Services.Domains.Roles.Dtos;

public record ClaimDto : BaseRecord
{
    public string Role { get; set; } = null!;
    public string ClaimType { get; set; } = null!;
}
