using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    public interface IPublisher
    {
        void Publish(Message message);
    }
}
