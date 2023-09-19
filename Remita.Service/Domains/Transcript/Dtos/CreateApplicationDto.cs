using Remita.Models.Domains.ApplicationFee.Enum;
using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Transcript.Dtos;

public record CreateApplicationDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Letters too few. Must be greater than one")]
    [MaxLength(25, ErrorMessage = " Letters too many. Must not be greater than twentyFive")]
    [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Name must contain only letters")]
    public string ApplicantFirstName { get; set; } = null!;
    [Required]
    [MinLength(2, ErrorMessage = "Letters too few. Must be greater than one")]
    [MaxLength(25, ErrorMessage = " Letters too many. Must not be greater than twentyFive")]
    [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Name must contain only letters")]
    public string ApplicantSurname { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string ApplicantEmail { get; set; } = null!;
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public string RegNumber { get; set; } = null!;
    [Required]
    public string Session { get; set; } = null!;

    [Required]
    public int ReceipientsCountryId { get; set; }
    [Required]
    public int ReceipientsStateId { get; set; }
    [Required]
    public string ReceipientsAddress { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string RecepientEmail { get; set; } = null!;
    [Required]
    public DeliveryFormat DeliveryFormat { get; set; }
}
