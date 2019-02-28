namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class RequiresSelfHostBuildingMessage : HdMessage
    {
        public long BuildId { get; set; }
        public int SelfHostConfiguration { get; set; }
    }
}