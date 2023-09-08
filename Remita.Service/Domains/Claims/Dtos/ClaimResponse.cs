using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remita.Services.Domains.Claims.Dtos
{
    public class ClaimResponse
    {
        public string Role { get; set; }
        public string ClaimType { get; set; }
    }

    public class ClaimRequest
    {
        public string Role { get; set; }
        public string ClaimType { get; set; }
    }
}
