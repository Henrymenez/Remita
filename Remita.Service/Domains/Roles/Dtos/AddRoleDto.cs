using Remita.Models.Utility;

namespace Remita.Services.Domains.Roles.Dtos;

public record AddRoleDto : BaseRecord
{
    public string userId { get; set; } = null!;
      public string roleName { get;set; } = null!;
}
