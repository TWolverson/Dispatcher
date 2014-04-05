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
            var dispatcher = new Dispatcher();
            var publisher = new Publisher(dispatcher);
            var boss = new Boss(publisher);

            dispatcher.Subscribe<WorkMessage>(new Worker());

            boss.DoStuff();
            Console.Read();
        }
    }

    public class Boss
    {
        private IPublisher _publisher;

        public Boss(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void DoStuff()
        {
            _publisher.Publish(new WorkMessage() { Work = "Do some damn work, Johan" });
        }
    }
}
