namespace HdProduction.MessageQueue.RabbitMq.Events.Project
{
  public class ProjectStoppingMessage : HdMessage
  {
    public long ProjectId { get; set; }
  }
}