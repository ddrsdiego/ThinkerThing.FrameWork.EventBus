using Rydo.Framework.MediatR.Eventos;

namespace ThinkerThings.Dominio.Usuarios.Events
{
    public class UsuarioAtualizadoEvent : IntegrationEvent
    {
        public UsuarioAtualizadoEvent(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; private set; }
    }
}