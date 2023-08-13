namespace Remita.Services.Domains.Auth.Dtos;

public record SignedInDto(string AccessToken, string RefreshToken, long ExpiryTimeStamp);
