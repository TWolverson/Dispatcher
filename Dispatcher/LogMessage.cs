using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    public class LogMessage : Message
    {
        public virtual string Message { get; private set; }

        public LogMessage(string message)
        {
            Message = message;
        }

        protected LogMessage()
        { }
    }

    public class ExceptionLogMessage : LogMessage
    {
        public override string Message
        {
            get
            {
                return _exception.Message;
            }
        }

        private Exception _exception;

        public ExceptionLogMessage(Exception exception)
        {
            _exception = exception;
        }
    }
}
