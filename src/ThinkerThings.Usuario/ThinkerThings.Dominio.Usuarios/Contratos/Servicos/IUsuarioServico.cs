using System.Collections.Generic;
using ThinkerThings.Dominio.Usuarios.Model;

namespace ThinkerThings.Dominio.Usuarios.Contratos.Servicos
{
    public interface IUsuarioServico
    {
        bool VerficarUsuarioCadastrado(Usuario usuario);

        void RegistrarUsuarios(IEnumerable<Usuario> usuarios);
        void AlterarUsuarios(IEnumerable<Usuario> usuarios);
    }
}
