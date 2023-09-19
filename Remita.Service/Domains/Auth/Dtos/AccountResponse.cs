using Remita.Models.Utility;

namespace Remita.Services.Domains.Auth.Dtos;

public record AccountResponse: BaseRecord
{
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public bool Success { get; set; }
    public string? Message { get; set; } 
}
