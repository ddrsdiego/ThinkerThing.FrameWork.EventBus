namespace EventBus
{
    public interface IEndpointSpecificationConfigurator
    {
        string QueueName { get; set; }
    }

    public class MessageConfiguration : IEndpointSpecificationConfigurator
    {
        public string QueueName { get; set; }
    }
}