namespace HdProduction.MessageQueue.RabbitMq.Events.Project
{
  public class ProjectStoppedByHelpDeskMessage : HdMessage
  {
    public long ProjectId { get; set; }
  }
}