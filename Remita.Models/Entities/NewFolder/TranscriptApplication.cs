using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMTS.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMTS.Models.Entities
{
   public class TranscriptApplication
    {
        public Guid Id { get; set; }
        public string ApplicantName { get; set; }
        [EmailAddress]
        public string ApplicantEmail { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string RegNumber { get; set; }
        public string Session { get; set; }
        public string ReceipientsLocation { get; set; }
        public int ReceipientsCountryId { get; set; }
        public int ReceipientsStateId { get; set; }

        [EmailAddress]
        public string? RecepientEmail { get; set; }

        public Guid? ApplicationInvoiceId { get; set; }
        public  ApplicationInvoice ApplicationInvoice { get; set; }
        public bool HasPaid { get; set; } = false;
        public DeliveryFormat DeliveryFormat { get; set; }
        public TranscriptApplicationStatus Status { get; set; }
        public string? RejectionReason { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        
    }
}
