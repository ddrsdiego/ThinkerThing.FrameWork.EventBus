using EventBus.Abstractions;
using System.Collections.Generic;

namespace EventBus
{
    public class QueueBindingEntity : IQueueBindingEntity
    {
        public QueueBindingEntity(IExchangeEntity exchange, IQueueEntity queue, string routingKey, IDictionary<string, object> arguments)
        {
            Exchange = exchange;
            Queue = queue;
            RoutingKey = routingKey;
            Arguments = arguments ?? new Dictionary<string, object>();
        }

        public IExchangeEntity Exchange { get; }
        public IQueueEntity Queue { get; }
        public string RoutingKey { get; }
        public IDictionary<string, object> Arguments { get; }
    }
}
