namespace Remita.Services.Domains.Roles.Dtos;

public record ClaimDto
{
    public string Role { get; set; } = null!;
    public string ClaimType { get; set; } = null!;
}
