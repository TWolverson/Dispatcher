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
            var printers = new[] { new Printer(bus, 1), new Printer(bus, 2), new Printer(bus, 3), new Printer(bus, 4) };

            var printerPool = printers
                .Select(printer => new WideningHandler<PrintJob, Message>(printer))
                .Select(printer => new QueuedHandler(printer, bus) { MaxQueueLength = 1 })
                .ToArray();

            var refillPool = printers
                .Select(printer => new WideningHandler<RefillPaper, Message>(printer))
                .Select(printer => new QueuedHandler(printer, bus) { MaxQueueLength = 1 })
                .ToArray();

            foreach (var printer in refillPool)
            {
                bus.Subscribe<RefillPaper>(new NarrowingHandler<RefillPaper, Message>(printer));
                // subscribe the printer directly to RefillPaper as we don't want to distribute that with the print jobs
            }

            var office = Enumerable.Range(0, 50)
                .Select(i => new Employee(bus, i))
                .ToArray();

            var loadBalancer = new RoundRobinLoadBalancer(printerPool);
            var printerRetryHandler = new RetryHandler(loadBalancer, bus);
            var retryManager = new RetryManager(bus);
            var timerService = new TimerService(bus);

            bus.Subscribe<FutureMessage>(timerService);
            bus.Subscribe<RetryMessage>(retryManager);
            bus.Subscribe<SuccessMessage>(retryManager);

            bus.Subscribe(new NarrowingHandler<PrintJob, Message>(printerRetryHandler));

            var console = new QueuedHandler(new WideningHandler<LogMessage, Message>(new ConsoleHandler()), bus);
            bus.Subscribe<LogMessage>(new NarrowingHandler<LogMessage, Message>(console));

            var porter = new Porter(bus);
            bus.Subscribe(porter);

            var converter = new LogConverter(bus);
            bus.Subscribe<PrintJob>(converter);
            bus.Subscribe<OutOfPaper>(converter);
            bus.Subscribe<PagePrinted>(converter);
            bus.Subscribe<RetryMessage>(converter);
        }
    }
}
