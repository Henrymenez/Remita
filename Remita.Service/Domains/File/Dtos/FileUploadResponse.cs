namespace Remita.Services.Domains.File.Dtos;

public record FileUploadResponse
{
    public int TotalResultsUploaded { get; set; }
    public IList<int> NotUploadedResultsRow { get; set; } = new List<int>();
}
