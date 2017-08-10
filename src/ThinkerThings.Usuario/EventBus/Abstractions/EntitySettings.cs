using System.Collections.Generic;

namespace EventBus.Abstractions
{
    public interface EntitySettings
    {
        string Name { get; }
        bool Durable { get; }
        bool AutoDelete { get; }
        IDictionary<string, object> Arguments { get; }
    }
}
