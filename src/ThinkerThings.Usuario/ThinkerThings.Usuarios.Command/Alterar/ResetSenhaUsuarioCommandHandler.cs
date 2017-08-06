using EventBus.Abstractions;
using MediatR;
using Rydo.Framework.MediatR.Handlres;
using ThinkerThings.Dominio.Usuarios.Commands;
using ThinkerThings.Dominio.Usuarios.Events;

namespace ThinkerThings.Usuarios.Command.Alterar
{
    public class ResetSenhaUsuarioCommandHandler : HandlerRequest<ResetSenhaUsuarioCommand, ResetSenhaUsuarioResponse>
    {
        private readonly IEventBus _eventBus;

        public ResetSenhaUsuarioCommandHandler(IMediator mediator, IEventBus eventBus) 
            : base(mediator)
        {
            _eventBus = eventBus;
        }

        public override ResetSenhaUsuarioResponse Handle(ResetSenhaUsuarioCommand message)
        {
            var resposte = message.Response;

            _eventBus.Publish(new UsuarioAtualizadoEvent("Teste de Bus"));

            return resposte;
        }
    }
}
