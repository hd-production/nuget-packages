using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HdProduction.MessageQueue.RabbitMq.Events;
using log4net;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HdProduction.MessageQueue.RabbitMq
{
  public interface IRabbitMqConsumer
  {
    IRabbitMqConsumer Subscribe<T>() where T : HdMessage;
    void StartConsuming();
  }

  public class RabbitMqConsumer : IRabbitMqConsumer
  {
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    
    private IModel _consumerChannel;
    private readonly Dictionary<string, Type> _supportedMessages;
    private readonly string _queueName;
    private readonly MessageHandler _messageHandler;
    private readonly IRabbitMqConnection _connection;

    public RabbitMqConsumer(string queueName, IServiceProvider serviceProvider, IRabbitMqConnection connection)
    {
      _queueName = queueName;
      _messageHandler = new MessageHandler(serviceProvider);
      _connection = connection;
      _supportedMessages = new Dictionary<string, Type>();
    }

    public IRabbitMqConsumer Subscribe<T>() where T : HdMessage
    {
      _supportedMessages.Add(typeof(T).Name, typeof(T));
      return this;
    }

    public void StartConsuming()
    {
      _consumerChannel = CreateConsumerChannel();

      foreach (string eventName in _supportedMessages.Keys)
      {
        _consumerChannel.QueueBind(_queueName, _connection.Exchange, eventName);
      }
    }

    private IModel CreateConsumerChannel()
    {
      if (!_connection.IsConnected)
      {
        _connection.Connect();
      }

      var channel = _connection.CreateChannel();

      channel.ExchangeDeclare(_connection.Exchange, ExchangeType.Direct, true);
      channel.QueueDeclare(_queueName, true, autoDelete: false, exclusive: false);

      var consumer = new AsyncEventingBasicConsumer(channel);
      consumer.Received += OnMessage;

      channel.BasicConsume(_queueName, false, consumer);

      return channel;
    }

    private async Task OnMessage(object sender, BasicDeliverEventArgs args)
    {
      try
      {
        string eventName = args.RoutingKey;
        string message = Encoding.UTF8.GetString(args.Body);

        if (!_supportedMessages.ContainsKey(eventName))
        {
          return;
        }

        await _messageHandler.ProcessAsync(_supportedMessages[eventName], message);
        _consumerChannel.BasicAck(args.DeliveryTag, false);
      }
      catch (Exception ex)
      {
        Log.Error("Rabbit Mq message processing error", ex);
      }
    }
  }
}