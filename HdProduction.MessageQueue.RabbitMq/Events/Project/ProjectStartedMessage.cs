namespace HdProduction.MessageQueue.RabbitMq.Events.Project
{
  public class ProjectStartedByHelpDeskMessage : HdMessage
  {
    public long ProjectId { get; set; }
  }
}