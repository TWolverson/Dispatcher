using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            ////Comments for the benefit of people trying to understand the pattern:

            //// This is basically the bare-minimum setup to allow actors to publish and subscribe
            //var dispatcher = new Dispatcher();
            //var publisher = new Publisher(dispatcher);
            //var retryManager = new RetryManager(dispatcher);
            //var boss = new Boss(retryManager);

            //// Set up what listens to what
            //dispatcher.Subscribe<WorkMessage>(new Worker(publisher));
            //dispatcher.Subscribe<LogMessage>(new ConsoleHandler());

            //// Make the pipeline do something by posting a message
            //boss.DoStuff();

            //Console.Read();
            var dispatcher = new Dispatcher();
            IBus bus = new Bus(dispatcher);



        }
    }

    public class Boss
    {
        private IHandle<Message> _publisher;

        public Boss(IHandle<Message> publisher)
        {
            _publisher = publisher;
        }

        public void DoStuff()
        {
            var work = new WorkMessage() { Work = "Do some damn work, Johan" };
            _publisher.Handle(new LogMessage(work.Work));
            _publisher.Handle(work);

        }
    }
}
