using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easychit_Api.Exeption_Handling
{
    public class Easychit_Exeption: Exception
    {
        public Easychit_Exeption()
        { }

        public Easychit_Exeption(string message)
            : base(message)
        { }

        public Easychit_Exeption(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
