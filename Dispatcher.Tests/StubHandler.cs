using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Tests
{
    public class StubHandler : IHandle<Message>
    {
        private IEnumerable<string> _handledMessages;

        public void Handle(Message message)
        {
            Console.WriteLine("Handling a message");
        }

        public StubHandler()
        {
            _handledMessages = new List<string>();
        }

        public IEnumerable<string> HandledMessages { get { return _handledMessages; } private set { _handledMessages = value; } }
    }
}
