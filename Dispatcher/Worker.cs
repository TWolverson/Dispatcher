using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    public class Worker : IHandle<WorkMessage>
    {
        public void Handle(WorkMessage message)
        {
            Console.WriteLine("Yes boss, doing some work");
        }
    }
}
