using MediatR;
using Rydo.Framework.MediatR.Handlres;
using ThinkerThings.Dominio.Usuarios.Commands;

namespace ThinkerThings.Usuarios.Command.Alterar
{
    public class AtualizarUsuarioCommandHandler : HandlerRequest<AtualizarUsuarioCommand, AtualizarUsuarioResponse>
    {
        public AtualizarUsuarioCommandHandler(IMediator mediator) 
            : base(mediator)
        {

        }

        public override AtualizarUsuarioResponse Handle(AtualizarUsuarioCommand message)
        {
            var response = message.Response;

            return response;
        }
    }
}
