using System.Threading.Tasks;
using HdProduction.MessageQueue.RabbitMq.Events;

namespace HdProduction.MessageQueue.RabbitMq.Stubs
{
    public class FakeMqPublisher : IRabbitMqPublisher
    {
        public void Publish(HdMessage @event)
        {
        }

        public Task PublishAsync(HdMessage @event)
        {
            return Task.CompletedTask;
        }
    }
}