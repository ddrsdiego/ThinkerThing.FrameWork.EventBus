using Rydo.Framework.MediatR.Command;
using Rydo.Framework.MediatR.Response;

namespace ThinkerThings.Dominio.Usuarios.Commands
{
    public class RegistrarUsuarioCommand : CommandMessage<RegistrarUsuarioResponse>
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }

        public override RegistrarUsuarioResponse Response => new RegistrarUsuarioResponse();
    }

    public class RegistrarUsuarioResponse : ResponseMessage
    {

    }
}
