namespace HdProduction.MessageQueue.RabbitMq.Events.Project
{
  public class ProjectStartingMessage : HdMessage
  {
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public DefaultProjectAdminSettings DefaultAdminSettings { get; set; }
  }

  public class DefaultProjectAdminSettings
  {
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Email { get; set; }
    public string Role { get; set; }
  }
}