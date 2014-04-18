using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    public class RetryLimitExceededException : Exception
    {
        private Message _message;

        public RetryLimitExceededException(Message message)
        {
            _message = message;
        }

        public override string ToString()
        {
            return "Message " + _message.ToString() + " exceeded the allowed number of retries";
        }
    }
}
