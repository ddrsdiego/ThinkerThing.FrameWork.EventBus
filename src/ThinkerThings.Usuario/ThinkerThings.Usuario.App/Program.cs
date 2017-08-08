using EventBus;
using EventBusRabbitMQ;
using System;
using ThinkerThings.Dominio.Usuarios.Events;

namespace ThinkerThings.Usuario.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var usuarioCriadoEvent = new UsuarioCriadoEvent
            {
                Nome = "Diego Dias Ribeiro da Silva",
                DataCriacao = DateTime.Now
            };

            var connection = GetConnection();
            var subManager = new InMemoryEventBusSubscriptionsManager();

            var eventbus = new EventBusRabbitMQ.EventBusRabbitMQ(connection, subManager, null);

            var key = Console.ReadKey();

            while (key.Key != ConsoleKey.Q)
            {
                eventbus.Publish(usuarioCriadoEvent);
                Console.ReadKey();
            }
        }

        private static IRabbitMQPersistentConnection GetConnection()
        {
            var factory = new RabbitMQ.Client.ConnectionFactory { HostName = "localhost" };
            return new DefaultRabbitMQPersistentConnection(factory);
        }
    }
}
