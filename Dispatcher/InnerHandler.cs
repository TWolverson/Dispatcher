using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class InnerHandler<T> : IHandle where T : Message
    {
        public void Handle(Message message)
        {
            _handler.Handle((T)message);
        }

        public InnerHandler(IHandle<T> handler)
        {
            _handler = handler;
        }

        private IHandle<T> _handler;
    }
}
