using Remita.Models.Domains.ApplicationFee.Enum;
using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Transcript.Dtos;

public record CreateApplicationDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Letters too few. Must be greater than one")]
    [MaxLength(25, ErrorMessage = " Letters too many. Must not be greater than twentyFive")]
    [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Name must contain only letters")]
    public string ApplicantFirstName { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Letters too few. Must be greater than one")]
    [MaxLength(25, ErrorMessage = " Letters too many. Must not be greater than twentyFive")]
    [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Name must contain only letters")]
    public string ApplicantSurname { get; set; }
    [Required]
    [EmailAddress]
    public string ApplicantEmail { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    [Required]
    public string RegNumber { get; set; }
    [Required]
    public string Session { get; set; }

    [Required]
    public int ReceipientsCountryId { get; set; }
    [Required]
    public int ReceipientsStateId { get; set; }
    [Required]
    public string ReceipientsAddress { get; set; }
    [Required]
    [EmailAddress]
    public string RecepientEmail { get; set; }
    [Required]
    public DeliveryFormat DeliveryFormat { get; set; }
}
