namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class SelfHostBuilt : HdEvent
    {
        public long ProjectId { get; set; }
        public string DownloadLink { get; set; }
    }
}