using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class Dispatcher
    {
        private readonly ConcurrentDictionary<Type, List<IHandle<Message>>> _subscriptions;

        public void Subscribe<TMessage>(IHandle<Message> handler)
        {
            _subscriptions.TryAdd(typeof(TMessage), new List<IHandle<Message>>());
            _subscriptions[typeof(TMessage)].Add(handler);
        }

        public void Publish<TMessage>(TMessage message)
        {
            var type = typeof(TMessage);
            while (type != typeof(object))
            {
                foreach (var handler in _subscriptions[type])
                {
                    handler.Handle(message as Message);
                }
            }
        }
    }
}
