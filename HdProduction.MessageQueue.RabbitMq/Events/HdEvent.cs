namespace HdProduction.MessageQueue.RabbitMq.Events
{
  public abstract class HdEvent
  {
    public string Name => GetType().Name;

    public override string ToString()
    {
      return Name;
    }
  }
}