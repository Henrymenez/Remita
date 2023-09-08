using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTS.Models.Entities
{
    public class StudentTranscript
    {
        [Key]
        public string MatricNumber { get; set; }

        public string TranscriptLink { get; set; }
    }
}
