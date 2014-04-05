namespace Dispatcher
{
    public class Publisher : IPublisher
    {
        private IHandle<Message> _handler;

        public Publisher(IHandle<Message> handler)
        {
            _handler = handler;
        }

        public void Publish(Message message)
        {
            _handler.Handle(message);
        }
    }
}