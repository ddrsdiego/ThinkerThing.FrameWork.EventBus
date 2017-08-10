namespace EventBus
{
    public interface IEndpointSpecificationConfigurator
    {
        string QueueName { get; set; }
        string ExchangeName { get; set; }
    }

    public class EndpointSpecificationConfigurator : IEndpointSpecificationConfigurator
    {
        public string QueueName { get; set; }

        public string ExchangeName { get; set; }
    }
}