using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printers
{
    public class OutOfPaperException : Exception
    {
        public override string Message
        {
            get
            {
                return "I'm out of paper!";
            }
        }
    }
}
