namespace Remita.Models.Entities.Domians.Transcript;

public class StudentTranscript : BaseEntity
{
    public string MatricNumber { get; set; } = null!;

    public string TranscriptLink { get; set; } = null!;
}
