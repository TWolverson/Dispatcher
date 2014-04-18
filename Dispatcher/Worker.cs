using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    public class Worker : IHandle<WorkMessage>
    {
        private IPublisher _publisher;

        private int messagesHandled = 0;
        public void Handle(WorkMessage message)
        {
            if (messagesHandled < 1)
            {
                // Console.WriteLine("Yes boss, doing some work");
                _publisher.Publish(new LogMessage("Yes boss, doing some work"));
            }
            else
            {
                throw new Exception("Sod off, I'm busy");
            }

        }

        public Worker(IPublisher publisher)
        {
            _publisher = publisher;
        }
    }
}
