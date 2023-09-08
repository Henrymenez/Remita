using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remita.Services.Domains.Transcript.Dtos;

public record TranscriptRequestDto
{
    public string RejectionReason { get; set; }
    public int Status { get; set; }
}
