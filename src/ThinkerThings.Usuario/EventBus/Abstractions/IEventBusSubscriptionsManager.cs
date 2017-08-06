using MediatR;
using Rydo.Framework.MediatR.Eventos;
using Rydo.Framework.MediatR.Handlres;
using System;

namespace EventBus.Abstractions
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        void Clear();

        void AddSubscription<T>(string eventName)
           where T : IRequest<Unit>;

        void RemoveSubscription<T, TH>()
           where T : IntegrationEvent
           where TH : IntegrationEventHandler<T>;

        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        string GetEventKey<T>();
    }
}
