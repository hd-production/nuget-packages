namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class ProjectRequiresSelfHostBuildingEvent : HdEvent
    {
        public long ProjectId { get; set; }
        public int SelfHostConfiguration { get; set; }
    }
}