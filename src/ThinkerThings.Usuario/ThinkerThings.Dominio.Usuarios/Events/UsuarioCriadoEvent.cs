using MediatR;
using System;

namespace ThinkerThings.Dominio.Usuarios.Events
{
    public class UsuarioCriadoEvent : IRequest<Unit>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
