using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printers
{
    public class PrintJob : Message
    {
        public int Pages { get; set; }
        public int ForEmployee { get; set; }
    }
}
