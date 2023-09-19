namespace Remita.Services.Domains.Transcript.Dtos;

public record TranscriptRequestDto
{
    public string RejectionReason { get; set; } = null!;
    public int Status { get; set; }
}
