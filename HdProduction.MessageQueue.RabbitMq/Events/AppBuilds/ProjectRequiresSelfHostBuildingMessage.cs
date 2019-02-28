namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class ProjectRequiresSelfHostBuildingMessage : HdMessage
    {
        public long ProjectId { get; set; }
        public int SelfHostConfiguration { get; set; }
    }
}