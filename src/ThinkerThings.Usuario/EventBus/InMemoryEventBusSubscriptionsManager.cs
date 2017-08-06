using EventBus.Abstractions;
using MediatR;
using Rydo.Framework.MediatR.Eventos;
using Rydo.Framework.MediatR.Handlres;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBus
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, Type> _eventTypes;

        public InMemoryEventBusSubscriptionsManager()
        {
            _eventTypes = new Dictionary<string, Type>();
        }

        public void Clear() => _eventTypes.Clear();
        public bool IsEmpty => !_eventTypes.Keys.Any();

        public void AddSubscription<T>(string eventName)
            where T : IRequest<Unit>
        {
            _eventTypes.Add(eventName, typeof(T));
        }

        public string GetEventKey<T>() => typeof(T).FullName;

        public Type GetEventTypeByName(string eventName) => _eventTypes[eventName];

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }

        public bool HasSubscriptionsForEvent(string eventName) => _eventTypes.ContainsKey(eventName);

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
