using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coordinare.Exceptions
{
    public class ExistsException : Exception
    {
        public ExistsException(string message) : base(message)
        {

        }
    }
}
