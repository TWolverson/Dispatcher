using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Printers
{
    public class Printer : IHandle<PrintJob>
    {
        private int _paper = 50;
        private IBus _bus;

        public Printer(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PrintJob message)
        {
            for (int page = 0; page < message.Pages; page++)
            {
                if (_paper > 0)
                {
                    _paper--;
                    Thread.Sleep(500);
                    _bus.Publish(new PagePrinted());
                }
                else
                {
                    _bus.Publish(new OutOfPaper());
                    return; //arguably we should throw an exception here and let a retry manager do something with it
                }
            }
        }
    }
}
