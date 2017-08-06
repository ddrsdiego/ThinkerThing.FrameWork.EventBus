using MediatR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ThinkerThings.Dominio.Usuarios.Events;

namespace ThinkerThings.Usuarios.Event
{
    public class UsuarioCriadoEventHandler : IAsyncRequestHandler<UsuarioCriadoEvent, Unit>
    {
        public Task<Unit> Handle(UsuarioCriadoEvent message)
        {
            Console.WriteLine($"Usuario sendo criado... {message.Nome}");
            Debug.WriteLine($"Usuario sendo criado... {message.Nome}");

            return Unit.Task;
        }
    }
}
