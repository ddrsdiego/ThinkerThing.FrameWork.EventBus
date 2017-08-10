using System.Collections.Generic;
using EventBus.Abstractions;

namespace EventBus.Abstractions
{
    public interface IQueueBindingEntity
    {
        IDictionary<string, object> Arguments { get; }
        IExchangeEntity Exchange { get; }
        IQueueEntity Queue { get; }
        string RoutingKey { get; }
    }
}