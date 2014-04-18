using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Printers
{
    public class Printer : IHandle<PrintJob>, IHandle<RefillPaper>
    {
        private int _paper = 50;
        private IBus _bus;
        private int _id;

        public Printer(IBus bus, int id)
        {
            _bus = bus;
            _id = id;
        }

        public void Handle(PrintJob message)
        {
            if (_paper == 0)
            {
                throw new OutOfPaperException();
            }

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
                    _bus.Publish(new OutOfPaper() { PrinterId = _id });
                    throw new OutOfPaperException();
                }
            }
        }

        public void Handle(RefillPaper message)
        {
            if (message.PrinterId == _id)
            {
                //that's me!
                _paper += message.Sheets;
            }
        }
    }
}
