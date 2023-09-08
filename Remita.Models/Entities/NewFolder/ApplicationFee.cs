using RMTS.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;


namespace RMTS.Models.Entities
{
   public class TranscriptApplicationFee : BaseEntity
    {
        public DeliveryFormat DeliveryFormat { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ApplicationFee { get; set; }  
         
    }
}
