using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;

namespace Dispatcher
{
    public class QueuedHandler : IHandle<Message>
    {
        private ConcurrentQueue<Message> _queue;
        private IHandle<Message> _next;
        private Thread _thread;
        private Stopwatch _stopwatch;

        public string Name { get; set; }

        public int MaxQueueLength { get; set; }

        public void Handle(Message message)
        {
            if (_queue.Count >= MaxQueueLength)
                throw new QueueFullException();
            _queue.Enqueue(message);
        }

        public QueuedHandler(IHandle<Message> next, IHandle<Message> bus)
        {
            _queue = new ConcurrentQueue<Message>();
            _next = next;
            _bus = bus;
            _thread = new Thread(Process);
            _thread.Start();
            _stopwatch = new Stopwatch();
            MaxQueueLength = int.MaxValue;
        }

        private IHandle<Message> _bus;

        public void Process()
        {
            while (true)
            {
                Message message;

                while (_queue.TryDequeue(out message))
                {
                    _stopwatch.Restart();

                    try
                    {
                        //Console.WriteLine(message.GetType() + " Queue: " + Name);
                        _next.Handle(message);
                    }
                    catch (Exception e)
                    {
                        //swallow
                        _bus.Handle(new ExceptionMessage(e));
                        // _bus.Handle(new FutureMessage(message, DateTimeOffset.Now + TimeSpan.FromMilliseconds(500)));
                    }


                    //if (_stopwatch.ElapsedMilliseconds > 200)
                    //    Console.WriteLine("This thing is slow: " + message.GetType() + " Queue: " + Name);
                }
            }
        }
    }
}