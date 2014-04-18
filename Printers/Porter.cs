using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printers
{
    public class Porter : IHandle<OutOfPaper>
    {
        private IBus _bus;

        public Porter(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(OutOfPaper message)
        {
            _bus.Publish(new RefillPaper { Sheets = 50, PrinterId = message.PrinterId });
        }
    }
}
