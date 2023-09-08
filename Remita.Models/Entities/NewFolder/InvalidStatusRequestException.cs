using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTS.Models.Entities
{
    public sealed class InvalidStatusRequestException : KeyNotFoundException
    {
        public InvalidStatusRequestException() 
            :base ("Invalid application status")
        {
            
        }
    }
}
