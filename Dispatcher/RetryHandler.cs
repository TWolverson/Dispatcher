using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class RetryHandler : IHandle<Message>
    {
        private IHandle<Message> _next;

        public RetryHandler(IHandle<Message> next, IBus bus)
        {
            _next = next;
            _bus = bus;
        }

        private IBus _bus;

        public void Handle(Message message)
        {
            try
            {
                _next.Handle(message);
                _bus.Publish(new SuccessMessage(message));
                return;
            }
            catch (Exception e)
            {
                //swallow
                _bus.Publish(new ExceptionMessage(e));
                _bus.Publish(new RetryMessage(message));
            }
        }
    }
}
