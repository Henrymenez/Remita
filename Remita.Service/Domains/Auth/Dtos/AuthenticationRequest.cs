namespace Remita.Services.Domains.Auth.Dtos;

public record AuthenticationResponse
{
    public JwtToken JwtToken { get; set; } = null!;
    public string UserType { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public bool TwoFactor { get; set; }
    public string UserId { get; set; } = null!;

}

public record JwtToken
{
    public string Token { get; set; } = null!;
    public DateTime Issued { get; set; }
    public DateTime? Expires { get; set; }
}
