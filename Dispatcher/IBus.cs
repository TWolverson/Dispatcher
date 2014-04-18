using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    public interface IBus : IPublisher
    {
        void Subscribe<TMessage>(IHandle<TMessage> handler) where TMessage : Message;
    }
}
