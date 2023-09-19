namespace Remita.Services.Domains.Transcript.Dtos;

public record TranscriptResponse
{
    public string RejectionReason { get; set; } = null!;
    public string Status { get; set; } = null!;
}
