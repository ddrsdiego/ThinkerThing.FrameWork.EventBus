namespace EventBus.Abstractions
{
    public interface IExchangeEntity : EntitySettings
    {
        string Type { get; }
    }
}