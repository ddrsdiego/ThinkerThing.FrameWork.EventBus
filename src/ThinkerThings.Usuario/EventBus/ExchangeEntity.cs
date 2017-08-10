using EventBus.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace EventBus
{
    public class ExchangeEntity : IExchangeEntity
    {
        public ExchangeEntity(string type, string name, bool durable, bool autoDelete, IDictionary<string, object> arguments)
        {
            Type = type;
            Name = name;
            Durable = durable;
            AutoDelete = autoDelete;
            Arguments = arguments ?? new Dictionary<string, object>();
        }

        public string Type { get; }
        public string Name { get; }
        public bool Durable { get; }
        public bool AutoDelete { get; }
        public IDictionary<string, object> Arguments { get; }

        public override string ToString()
        {
            return string.Join(", ", new[]
            {
                $"name: {Name}",
                $"type: {Type}",
                Durable ? "durable" : "",
                AutoDelete ? "auto-delete" : "",
                string.Join(", ", Arguments.Select(x => $"{x.Key}: {x.Value}"))
            }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}
