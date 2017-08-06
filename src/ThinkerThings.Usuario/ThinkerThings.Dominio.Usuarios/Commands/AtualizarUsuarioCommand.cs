using Rydo.Framework.MediatR.Command;
using Rydo.Framework.MediatR.Response;

namespace ThinkerThings.Dominio.Usuarios.Commands
{
    public class AtualizarUsuarioCommand : CommandMessage<AtualizarUsuarioResponse>
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public override AtualizarUsuarioResponse Response => new AtualizarUsuarioResponse();
    }

    public class AtualizarUsuarioResponse : ResponseMessage
    {

    }
}
