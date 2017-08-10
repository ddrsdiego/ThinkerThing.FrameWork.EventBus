using EventBus.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace EventBus
{
    public class QueueEntity : IQueueEntity
    {
        public QueueEntity(string name, bool exclusive, bool durable, bool autoDelete, IDictionary<string, object> arguments)
        {
            Name = name;
            Exclusive = exclusive;
            Durable = durable;
            AutoDelete = autoDelete;
            Arguments = arguments ?? new Dictionary<string, object>();
        }

        public string Name { get; }
        public bool Exclusive { get; }
        public bool Durable { get; }
        public bool AutoDelete { get; }
        public IDictionary<string, object> Arguments { get; }

        public override string ToString()
        {
            return string.Join(", ", new[]
            {
                $"name: {Name}",
                Durable ? "durable" : "",
                AutoDelete ? "auto-delete" : "",
                Exclusive ? "exclusive" : "",
                string.Join(", ", Arguments.Select(x => $"{x.Key}: {x.Value}"))
            }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}
