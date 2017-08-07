using EventBus.Abstractions;
using MediatR;
using Rydo.Framework.MediatR.Handlres;
using ThinkerThings.Dominio.Usuarios.Commands;

namespace ThinkerThings.Usuarios.Command.Alterar
{
    public class AtualizarUsuarioCommandHandler : HandlerRequest<AtualizarUsuarioCommand, AtualizarUsuarioResponse>
    {
        private readonly IEventBus _eventBus;

        public AtualizarUsuarioCommandHandler(IMediator mediator, IEventBus eventBus) 
            : base(mediator)
        {

        }

        public override AtualizarUsuarioResponse Handle(AtualizarUsuarioCommand message)
        {
            var response = message.Response;

            //Salvar no Banco.

            //_eventBus.Publish()

            return response;
        }
    }
}
