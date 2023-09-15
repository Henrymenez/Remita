using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Remita.Models.Domains.ApplicationFee.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remita.Models.Entities.Domians.Transcript;

public class TranscriptApplicationFee : BaseEntity
{
    public DeliveryFormat DeliveryFormat { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal ApplicationFee { get; set; }
}
