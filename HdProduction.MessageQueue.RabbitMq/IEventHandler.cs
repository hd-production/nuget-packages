using System.Threading.Tasks;
using HdProduction.MessageQueue.RabbitMq.Events;

namespace HdProduction.MessageQueue.RabbitMq
{
  public interface IEventHandler<in T> where T : HdEvent
  {
    Task HandleAsync(T ev);
  }
}