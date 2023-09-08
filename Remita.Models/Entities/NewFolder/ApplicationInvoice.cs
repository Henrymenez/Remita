using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTS.Models.Entities
{
    public class ApplicationInvoice
    {
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? ApplicationFee { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? DeliveryFee { get; set; }
        public bool HasPaid { get; set; } = false;
        public string ApplicationId { get; set; }
        public virtual TranscriptApplication Application { get; set; }
    }
}
