using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher.Tests
{
    class StubDispatcher : IHandle<Message>
    {
        public void Handle(Message message)
        {
            //Do nothing
        }
    }
}
