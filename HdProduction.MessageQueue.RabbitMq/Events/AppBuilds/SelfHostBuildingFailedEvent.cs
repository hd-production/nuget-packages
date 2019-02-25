namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class SelfHostBuildingFailedEvent : HdEvent
    {
        public long ProjectId { get; set; }
        public string Exception { get; set; }
    }
}