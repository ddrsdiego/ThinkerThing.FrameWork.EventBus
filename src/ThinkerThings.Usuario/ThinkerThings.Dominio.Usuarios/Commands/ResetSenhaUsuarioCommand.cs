using Rydo.Framework.MediatR.Command;
using Rydo.Framework.MediatR.Response;

namespace ThinkerThings.Dominio.Usuarios.Commands
{
    public class ResetSenhaUsuarioCommand : CommandMessage<ResetSenhaUsuarioResponse>
    {
        public ResetSenhaUsuarioCommand(string email)
        {
            Email = email;
        }

        public int IdUsuario { get; set; }
        public string Email { get; private set; }

        public override ResetSenhaUsuarioResponse Response => new ResetSenhaUsuarioResponse();
    }

    public class ResetSenhaUsuarioResponse : ResponseMessage
    {

    }
}
