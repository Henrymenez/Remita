namespace Remita.Services.Domains.User.Dtos;

public record UserResponse
{
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public bool Success { get; set; }
    public string? Message { get; set; }
}
