using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HdProduction.MessageQueue.RabbitMq.Events;
using HdProduction.MessageQueue.RabbitMq.Helpers;
using Newtonsoft.Json;
using RabbitMQ.Client.Exceptions;
using IBasicProperties = RabbitMQ.Client.IBasicProperties;

namespace HdProduction.MessageQueue.RabbitMq
{
  public interface IRabbitMqPublisher
  {
    void Publish(HdMessage @event);
    Task PublishAsync(HdMessage @event);
  }

  public class RabbitMqPublisher : IRabbitMqPublisher
  {
    private readonly IRabbitMqConnection _connection;

    public RabbitMqPublisher(IRabbitMqConnection connection)
    {
      _connection = connection;
    }

    public void Publish(HdMessage @event)
    {
      using (var channel = _connection.CreateChannel())
      {
        channel.QueueDeclare(_connection.Exchange, true, false, false, null);
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
        RetryPolicy.ExecuteAndCapture<SocketException, BrokerUnreachableException>(5, TimeSpan.FromSeconds(3),
          () =>
          {
            IBasicProperties basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = true;
            channel.BasicPublish(_connection.Exchange, @event.Name, true, basicProperties, body);
          });
      }
    }

    public Task PublishAsync(HdMessage @event)
    {
      return Task.Run(() => Publish(@event));
    }
  }
}