using EventBus.RabbitMQ;
using EventBusRabbitMQ;
using System;
using ThinkerThings.Dominio.Usuarios.Events;

namespace ThinkerThings.Usuario.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //decimal investedValue = 5100;
            //decimal minimumInitialApplication = 5000;
            //decimal minimumMovement = 10000;
            //bool hasFundOnCustody = true;

            //Validate(investedValue, minimumInitialApplication, minimumMovement, hasFundOnCustody);

            var usuarioCriadoEvent = new UsuarioCriadoEvent
            {
                Nome = "Diego Dias Ribeiro da Silva",
                DataCriacao = DateTime.Now
            };

            var eventbus = new EventBusRabbitMQ.EventBusRabbitMQ(GetConnection(), null);

            var key = Console.ReadKey();

            while (key.Key != ConsoleKey.Q)
            {
                eventbus.Publish(usuarioCriadoEvent);
                key = Console.ReadKey();
            }
        }

        private static void Validate(decimal investedValue, decimal minimumInitialApplication, decimal minimumMovement, bool hasFundOnCustody)
        {

            if (!IsMinimunInitialApplicationCriteriaOk(hasFundOnCustody, investedValue, minimumInitialApplication))
            {
                Console.WriteLine(string.Format("FUNDO 'ValidateFundInvestmentValue' - Valor inferior à Aplicação Inicial do Fundo : Cliente [{0}], Fundo [{1}]", 80030, 123456));
                return;
            }

            if (!IsMinimunMovementCriteriaOk(investedValue, minimumMovement, hasFundOnCustody))
            {
                Console.WriteLine(string.Format("FUNDO 'IsMinimunMovementCriteriaOk' - Valor inferior à Movimentação Mínima do Fundo: Cliente [{0}], Fundo [{1}]", 80030, 123456));
                return;
            }
        }

        private static IRabbitMQPersistentConnection GetConnection()
        {
            var factory = new RabbitMQ.Client.ConnectionFactory { HostName = "localhost" };
            return new DefaultRabbitMQPersistentConnection(factory);
        }

        private static bool IsMinimunMovementCriteriaOk(decimal investedValue, decimal minimunMovement, bool hasFundOnCustody)
        {
            const bool IGNORE_VALIDATION = true;

            return hasFundOnCustody ? investedValue >= minimunMovement : IGNORE_VALIDATION;
        }

        private static bool IsMinimunInitialApplicationCriteriaOk(bool hasFundOnCustody, decimal investedValue, decimal minimunInitialApplication)
        {
            return investedValue >= minimunInitialApplication || hasFundOnCustody;
        }
    }
}
