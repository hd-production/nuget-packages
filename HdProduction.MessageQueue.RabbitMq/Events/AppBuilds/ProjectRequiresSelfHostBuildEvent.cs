namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class ProjectRequiresSelfHostBuildEvent : HdEvent
    {
        public long ProjectId { get; set; }
        public int SelfHostConfiguration { get; set; }
    }
}