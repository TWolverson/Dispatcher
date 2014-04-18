using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace Dispatcher
{
    public class TimerService : IHandle<FutureMessage>
    {
        private ConcurrentDictionary<DateTimeOffset, List<Message>> _messages;

        private IBus _bus;

        private Timer _timer;

        public TimerService(IBus bus)
        {
            _bus = bus;
            _messages = new ConcurrentDictionary<DateTimeOffset, List<Message>>();
            _timer = new Timer(500);
            _timer.Start();
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<Message> messages;
            var now = DateTimeOffset.UtcNow;
            foreach (var key in _messages.Keys.Where(x => x <= now).ToArray())
            {
                if (_messages.TryGetValue(key, out messages))
                {
                    foreach (var message in messages)
                    {
                        _bus.Publish(message);
                    }
                }
                _messages.TryRemove(key, out messages);
            }
        }

        public void Handle(FutureMessage message)
        {
            var retryAt = Round(message.RetryAt);

            _messages.TryAdd(retryAt, new List<Message>());
            List<Message> messages;
            if (_messages.TryGetValue(retryAt, out messages))
            {
                messages.Add(message.Message);
            }
        }

        private DateTimeOffset Round(DateTimeOffset source)
        {
            var mils = source.Millisecond - (source.Millisecond % 500);
            var retryAt = new DateTimeOffset(source.Year,
                source.Month,
                source.Day,
                source.Hour, source.Minute, source.Second,
                mils,
                source.Offset);
            return retryAt;
        }
    }
}
