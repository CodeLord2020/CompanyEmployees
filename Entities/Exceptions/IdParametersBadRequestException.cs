using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class IdParametersBadRequestException : BadRequestException
    {
        public IdParametersBadRequestException() : base("Parameter ids is null")
        {
            
        }
    }
}