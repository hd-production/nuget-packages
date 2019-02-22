using RabbitMQ.Client;

namespace HdProduction.MessageQueue.RabbitMq
{
  public interface IRabbitMqConnection
  {
    string Exchange { get; }
    IModel CreateChannel();
    void Connect();
    bool IsConnected { get; }
  }
}