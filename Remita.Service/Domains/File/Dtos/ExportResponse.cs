namespace Remita.Services.Domains.File.Dtos;

public record ExportResponse
{
    public string Message { get; set; } = null!;
    public bool success { get; set; }
    public string? Error { get; set; }
}
