namespace HdProduction.MessageQueue.RabbitMq.Events.AppBuilds
{
    public class SelfHostBuiltEvent : HdEvent
    {
        public long ProjectId { get; set; }
        public string DownloadLink { get; set; }
    }
}