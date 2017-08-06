using MediatR;
using System;
using ThinkerThings.Dominio.Usuarios.Events;

namespace ThinkerThings.Usuarios.Event
{
    public class UsuarioAtualizadoEventHandler : IRequestHandler<UsuarioAtualizadoEvent, Unit>
    {
        public Unit Handle(UsuarioAtualizadoEvent message)
        {
            Console.WriteLine("Atualizando o usuário...");

            return Unit.Value;
        }
    }
}