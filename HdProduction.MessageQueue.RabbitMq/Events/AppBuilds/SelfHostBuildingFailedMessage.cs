namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class SelfHostBuildingFailedMessage : HdMessage
    {
        public long ProjectId { get; set; }
        public string Exception { get; set; }
    }
}