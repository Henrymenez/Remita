using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remita.Models.Entities.Domians.Delivery;

internal class DeliveryFee : BaseEntity
{
    public int CountryId { get; set; }
    public string Country { get; set; }
    public int StateId { get; set; }
    public string StateName { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal Fee { get; set; }
}
