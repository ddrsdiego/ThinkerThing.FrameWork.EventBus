using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using MediatR;
using RabbitMQ.Client;
using Rydo.Framework.MediatR.Eventos;
using Rydo.Framework.MediatR.Handlres;
using SimpleInjector;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ThinkerThings.Usuarios.IoC
{
    public static class EventBusContainer
    {
        public static void Registrar(Container container)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            container.Register<IRabbitMQPersistentConnection>(() => new DefaultRabbitMQPersistentConnection(factory), Lifestyle.Singleton);

            container.Register<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>(Lifestyle.Singleton);

            container.Register<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(Lifestyle.Singleton);
        }
    }
}
