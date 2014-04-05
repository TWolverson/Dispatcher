using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatcher.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var handler = new QueuedHandler(new StubHandler(), new StubDispatcher());
            handler.Handle(new StubMessage());
        }
    }
}
