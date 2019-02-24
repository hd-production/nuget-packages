using HdProduction.MessageQueue.RabbitMq.Events;

namespace HdProduction.MessageQueue.RabbitMq.Stubs
{
    public class FakeMqConsumer : IRabbitMqConsumer
    {
        public IRabbitMqConsumer Subscribe<T>() where T : HdEvent
        {
            return this;
        }

        public void StartConsuming()
        {
        }
    }
}