using MediatR;
using System;
using ThinkerThings.Dominio.Usuarios.Events;

namespace ThinkerThings.Usuarios.Event
{
    public class UsuarioCriadoEventHandler : IRequestHandler<UsuarioCriadoEvent, Unit>
    {
        public Unit Handle(UsuarioCriadoEvent message)
        {
            Console.WriteLine("Usuario sendo criado...");

            return Unit.Value;
        }
    }
}
