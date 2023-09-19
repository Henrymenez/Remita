using Remita.Models.Domains.ApplicationFee.Enum;
using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Results.Dtos;

public record ResultRequest
{
    [Required]
    public Guid ResultId { get; set; }
    [Required]
    public ApprovalStatus ApprovalStatus { get; set; }
    [Required]
    public string ApproverId { get; set; }
}
