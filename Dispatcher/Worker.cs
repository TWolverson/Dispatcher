using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    public class Worker : IHandle<WorkMessage>
    {
        private IPublisher _publisher;
        public void Handle(WorkMessage message)
        {
           // Console.WriteLine("Yes boss, doing some work");
            _publisher.Publish(new LogMessage("Yes boss, doing some work"));
        }

        public Worker(IPublisher publisher)
        {
            _publisher = publisher;
        }
    }
}
