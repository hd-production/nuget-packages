using System.Threading.Tasks;
using HdProduction.MessageQueue.RabbitMq.Events;

namespace HdProduction.MessageQueue.RabbitMq
{
  public interface IMessageHandler<in T> where T : HdMessage
  {
    Task HandleAsync(T ev);
  }
}