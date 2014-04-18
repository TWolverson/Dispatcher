using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printers
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new Bus(new Dispatcher.Dispatcher());
            var printers = new[] { new Printer(bus), new Printer(bus), new Printer(bus), new Printer(bus) }
                .Select(printer => new WideningHandler<PrintJob, Message>(printer))
                .Select(printer => new QueuedHandler(printer, bus))
                .ToArray();

            var office = Enumerable.Range(0, 50)
                .Select(i => new Employee(bus, i))
                .ToArray();

            var loadBalancer = new RoundRobinLoadBalancer(printers);
            bus.Subscribe<PrintJob>(new NarrowingHandler<PrintJob, Message>(loadBalancer));

            var console = new QueuedHandler(new WideningHandler<LogMessage, Message>(new ConsoleHandler()), bus);
            bus.Subscribe<LogMessage>(new NarrowingHandler<LogMessage, Message>(console));

            var converter = new LogConverter(bus);
            bus.Subscribe<PrintJob>(converter);
            bus.Subscribe<OutOfPaper>(converter);
            bus.Subscribe<PagePrinted>(converter);
        }
    }
}
