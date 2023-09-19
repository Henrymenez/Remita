using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Remita.Models.Entities.Domians.Transcript;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remita.Models.Entities.Domians.Invoice;

public class InvoiceApplication : BaseEntity
{
    [Column(TypeName = "decimal(18,4)")]
    public decimal? ApplicationFee { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal? DeliveryFee { get; set; }
    public bool HasPaid { get; set; } = false;
    public Guid ApplicationId { get; set; }
    public virtual TranscriptApplication Application { get; set; } = null!;
}
