using System.Threading.Tasks;
using HdProduction.MessageQueue.RabbitMq.Events;

namespace HdProduction.MessageQueue.RabbitMq.Stubs
{
    public class FakeMqPublisher : IRabbitMqPublisher
    {
        public void Publish(HdEvent @event)
        {
        }

        public Task PublishAsync(HdEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}