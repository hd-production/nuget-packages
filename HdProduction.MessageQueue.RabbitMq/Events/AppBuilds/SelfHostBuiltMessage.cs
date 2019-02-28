namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class SelfHostBuiltMessage : HdMessage
    {
        public long BuildId { get; set; }
        public string DownloadLink { get; set; }
    }
}