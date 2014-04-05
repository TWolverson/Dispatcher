using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dispatcher;

namespace Dispatcher.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dispatcher = new Dispatcher(); var handler = new StubHandler();
            dispatcher.Subscribe<Message>(new NarrowingHandler<Message, Message>(handler));
            dispatcher.Handle(new StubMessage());
            Assert.AreEqual("Handled a message", handler.HandledMessages);
        }
    }
}
