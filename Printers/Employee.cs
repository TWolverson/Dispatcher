using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Printers
{
    public class Employee
    {
        private IBus _bus;
        private int _employeeNumber;
        private Thread _thread;

        public Employee(IBus bus, int employeeNumber)
        {
            _bus = bus;
            _employeeNumber = employeeNumber;
            _thread = new Thread(GoToWork);
            _thread.Start();
        }

        public void GoToWork()
        {
            // start up a thread and periodically submit jobs to a printer.
            // that counts as 'work', right?
            var rand = new Random(_employeeNumber);
            while (true)
            {
                Thread.Sleep(rand.Next(10000));
                _bus.Publish(new PrintJob() { Pages = rand.Next(6) + 1, ForEmployee = _employeeNumber });
            }
        }
    }
}
