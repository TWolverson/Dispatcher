using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    public class SuccessMessage : Message
    {
        public Message Message { get; private set; }
        public SuccessMessage(Message wrappedMessage)
        {
            Message = wrappedMessage;
        }
    }
}
