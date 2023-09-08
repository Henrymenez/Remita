namespace Remita.Services.Domains.Transcript.Dtos;

public record CreateApplicationResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string ApplicationId { get; set; }
}
