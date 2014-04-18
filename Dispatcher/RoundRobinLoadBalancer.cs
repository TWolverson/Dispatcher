using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class RoundRobinLoadBalancer : IHandle<Message>
    {
        private IHandle<Message>[] _handlers;

        public RoundRobinLoadBalancer(IHandle<Message>[] handlers)
        {
            _handlers = handlers;
        }

        private int next = -1;

        public void Handle(Message message)
        {
            var index = ++next % _handlers.Length;
            _handlers[index].Handle(message);
        }
    }
}
