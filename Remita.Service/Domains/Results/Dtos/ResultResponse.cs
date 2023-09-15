namespace Remita.Services.Domains.Results.Dtos;

public record ResultResponse
{
    public string Message { get; set; }
    public bool IsSuccessful { get; set; }
}
