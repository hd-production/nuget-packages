using HdProduction.MessageQueue.RabbitMq.Events;

namespace HdProduction.MessageQueue.RabbitMq.Stubs
{
    public class FakeMqConsumer : IRabbitMqConsumer
    {
        public IRabbitMqConsumer Subscribe<T>() where T : HdMessage
        {
            return this;
        }

        public void StartConsuming()
        {
        }
    }
}