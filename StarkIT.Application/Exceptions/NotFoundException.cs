using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarkIT.Application.Exceptions
{
    public class NotFoundException: ApplicationException
    {
        public NotFoundException() : base("The database seed was not found") { }
    }
}
