using System.Net;

namespace Remita.Services.Domains.ApplicationFee.Dtos;

public record ApplicationFeeResponseDto
{
    public HttpStatusCode Status { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public string? ApplicationFeeId { get; set; }
}
