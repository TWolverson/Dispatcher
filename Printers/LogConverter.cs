﻿using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printers
{
    public class LogConverter : IHandle<PrintJob>, IHandle<OutOfPaper>, IHandle<PagePrinted>, IHandle<RetryMessage>, IHandle<ExceptionMessage>
    {
        private IBus _bus;
        public LogConverter(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PrintJob message)
        {
            _bus.Publish(new LogMessage("A print job was submitted by " + message.ForEmployee + " for " + message.Pages + " pages."));
        }

        public void Handle(PagePrinted message)
        {
            _bus.Publish(new LogMessage("A page was printed."));
        }

        public void Handle(OutOfPaper message)
        {
            _bus.Publish(new LogMessage("Oh noes; out of paper!"));
        }

        public void Handle(RetryMessage message)
        {
            _bus.Publish(new LogMessage("Retrying: " + message.Message.GetType().ToString()));
        }

        public void Handle(ExceptionMessage message)
        {
            _bus.Publish(new LogMessage("An exception was thrown: " + message.Exception.Message));
        }
    }
}
