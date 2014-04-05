using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class ConsoleHandler : IHandle<LogMessage>
    {
        public void Handle(LogMessage message)
        {
            Console.WriteLine(message.Message);
        }
    }
}
