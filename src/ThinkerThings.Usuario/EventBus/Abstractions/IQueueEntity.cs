namespace EventBus.Abstractions
{
    public interface IQueueEntity : EntitySettings
    {
        bool Exclusive { get; }
    }
}
