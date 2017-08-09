using MediatR;
using Rydo.Framework.MediatR.Eventos;
using System;

namespace EventBus.Abstractions
{
    public interface IEventBus : IDisposable
    {
        void Subscribe<T>() where T : IntegrationEvent;

        void Subscribe<T>(Action<IEndpointSpecificationConfigurator> configurator) where T : IntegrationEvent;

        void Publish(IntegrationEvent @event);
    }
}
