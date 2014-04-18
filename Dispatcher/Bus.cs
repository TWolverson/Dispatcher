using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class Bus : IBus
    {
        private Dispatcher _dispatcher;
        private QueuedHandler _queue;

        public Bus(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _queue = new QueuedHandler(_dispatcher, this);
        }

        public void Subscribe<TMessage>(IHandle<TMessage> handler) where TMessage : Message
        {
            _dispatcher.Subscribe(handler);
        }

        public void Publish(Message message)
        {
            _queue.Handle(message);
        }
    }
}
