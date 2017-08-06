using MediatR;

namespace ThinkerThings.Dominio.Usuarios.Events
{
    public class UsuarioAtualizadoEvent : IRequest<Unit>
    {
        public UsuarioAtualizadoEvent(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }
    }
}