namespace Remita.Services.Domains.Results.Dtos;

public record ResultResponse
{
    public string Message { get; set; } = null!;
    public bool IsSuccessful { get; set; }
}
