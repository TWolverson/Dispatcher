using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class NarrowingHandler<TDerived, TBase> : IHandle<TDerived>
        where TDerived : TBase
        where TBase : Message
    {
        private IHandle<TBase> _handler;

        public void Handle(TDerived message)
        {
            _handler.Handle(message);
        }

        public NarrowingHandler(IHandle<TBase> handler)
        {
            _handler = handler;
        }
    }
}
