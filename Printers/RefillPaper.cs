using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printers
{
    public class RefillPaper : Message
    {
        public int Sheets { get; set; }

        public int PrinterId { get; set; }
    }
}
