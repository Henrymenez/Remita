﻿namespace Remita.Services.Domains.Auth.Dtos;

public record AuthenticationResponse
{
    public JwtToken JwtToken { get; set; }
    public string UserType { get; set; }
    public string FullName { get; set; }
    public bool TwoFactor { get; set; }
    public string UserId { get; set; }

}

public record JwtToken
{
    public string Token { get; set; }
    public DateTime Issued { get; set; }
    public DateTime? Expires { get; set; }
}