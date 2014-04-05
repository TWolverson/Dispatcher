using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    public class ExceptionMessage : Message
    {
        private Exception _exception;
        public ExceptionMessage(Exception exception)
        {
            _exception = exception;
        }
    }
}
