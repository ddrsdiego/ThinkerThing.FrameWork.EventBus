using MediatR;
using Rydo.Framework.MediatR.Handlres;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ThinkerThings.Dominio.Usuarios.Events;

namespace ThinkerThings.Usuarios.Event
{
    public class UsuarioCriadoEventHandler : IntegrationEventHandler<UsuarioCriadoEvent, Unit>
    {
        //public Task<Unit> Handle(UsuarioCriadoEvent message)
        //{
        //    //Thread.Sleep(2000);

        //    Console.WriteLine($"Usuario sendo criado... {message.Nome}");
        //    Debug.WriteLine($"Usuario sendo criado... {message.Nome}");

        //    return Unit.Task;
        //}
        public UsuarioCriadoEventHandler(IMediator mediator) 
            : base(mediator)
        {

        }

        public override Task<Unit> Handle(UsuarioCriadoEvent message)
        {
            //    //Thread.Sleep(2000);

            Console.WriteLine($"Usuario sendo criado... {message.Nome}");
            Debug.WriteLine($"Usuario sendo criado... {message.Nome}");

            return Unit.Task;
        }
    }
}
