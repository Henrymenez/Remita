
using Remita.Models.Domains.ApplicationFee.Enum;
using Remita.Models.Domains.Course.Enum;
using Remita.Models.Entities.Domians.Invoice;
using System.ComponentModel.DataAnnotations;

namespace Remita.Models.Entities.Domians.Transcript;

public class TranscriptApplication : BaseEntity
{
    public string ApplicantName { get; set; } = null!;
    [EmailAddress]
    public string ApplicantEmail { get; set; } = null!;
    [Phone]
    public string PhoneNumber { get; set; } = null!;
    public string RegNumber { get; set; } = null!;
    public string Session { get; set; } = null!;
    public string ReceipientsLocation { get; set; } = null!;
    public int ReceipientsCountryId { get; set; }
    public int ReceipientsStateId { get; set; }
    [EmailAddress]
    public string? RecepientEmail { get; set; }
    public Guid? ApplicationInvoiceId { get; set; }
    public virtual InvoiceApplication ApplicationInvoice { get; set; } = null!;
    public bool HasPaid { get; set; } = false;
    public DeliveryFormat DeliveryFormat { get; set; }
    public TranscriptApplicationStatus Status { get; set; }
    public string? RejectionReason { get; set; }
    public string? Message { get; set; }

}
