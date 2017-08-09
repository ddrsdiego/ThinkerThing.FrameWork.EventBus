using EventBus;
using EventBus.Abstractions;
using EventBus.RabbitMQ;
using EventBusRabbitMQ;
using RabbitMQ.Client;
using SimpleInjector;

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
