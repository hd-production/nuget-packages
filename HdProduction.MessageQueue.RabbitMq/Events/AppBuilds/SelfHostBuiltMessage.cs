namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class SelfHostBuiltMessage : HdMessage
    {
        public long ProjectId { get; set; }
        public string DownloadLink { get; set; }
    }
}