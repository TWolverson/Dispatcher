using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class RetryManager : IHandle<RetryMessage>, IHandle<SuccessMessage>
    {
        private const int maxRetries = 10;

        private ConcurrentDictionary<Message, int> _retries;

        private IHandle<Message> _bus;

        public RetryManager(IHandle<Message> bus)
        {
            _retries = new ConcurrentDictionary<Message, int>();
            _bus = bus;
        }

        public void Handle(RetryMessage message)
        {
            int numRetriesAttempted;

            _retries.AddOrUpdate(message.Message, m => 1, (m, i) => i + 1);
            if (_retries.TryGetValue(message.Message, out numRetriesAttempted))
            {
                if (numRetriesAttempted > maxRetries)
                {
                    _bus.Handle(new ExceptionMessage(new RetryLimitExceededException(message.Message)));
                    _retries.TryRemove(message.Message, out numRetriesAttempted);
                    return;
                }
                _bus.Handle(new FutureMessage(message.Message, DateTimeOffset.UtcNow + TimeSpan.FromMilliseconds(50)));
            }
        }

        public void Handle(SuccessMessage message)
        {
            int numRetriesAttempted;
            if (_retries.TryGetValue(message.Message, out numRetriesAttempted))
            {
                _retries.TryRemove(message.Message, out numRetriesAttempted);
            }
        }
    }
}
