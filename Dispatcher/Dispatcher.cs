using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class Dispatcher : IHandle<Message>
    {
        private readonly ConcurrentDictionary<Type, List<IHandle>> _subscriptions;

        public void Subscribe<TMessage>(IHandle<TMessage> handler) where TMessage : Message
        {
            _subscriptions.TryAdd(typeof(TMessage), new List<IHandle>());
            _subscriptions[typeof(TMessage)].Add(new InnerHandler<TMessage>(handler));
        }

        public Dispatcher()
        {
            _subscriptions = new ConcurrentDictionary<Type, List<IHandle>>();
        }

        public void Handle(Message message)
        {
            var type = message.GetType();
            while (type != typeof(object))
            {
                List<IHandle> subscriptions;
                if (_subscriptions.TryGetValue(type, out subscriptions))
                {
                    foreach (var handler in subscriptions)
                    {
                        handler.Handle(message as Message);
                    }
                }
                type = type.BaseType;
            }
        }
    }
}
