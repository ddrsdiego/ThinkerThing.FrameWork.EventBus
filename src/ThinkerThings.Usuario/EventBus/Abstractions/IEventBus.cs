using MediatR;
using System;

namespace EventBus.Abstractions
{
    public interface IEventBus : IDisposable
    {
        void Subscribe<T>() where T : IRequest<Unit>;

        void Publish(IRequest<Unit> @event);
    }
}
