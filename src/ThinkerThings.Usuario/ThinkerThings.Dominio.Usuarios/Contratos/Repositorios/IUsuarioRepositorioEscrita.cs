using System.Collections.Generic;
using ThinkerThings.Dominio.Usuarios.Model;

namespace ThinkerThings.Dominio.Usuarios.Contratos.Repositorios
{
    public interface IUsuarioRepositorioEscrita
    {
        void AlterarUsuarios(IEnumerable<Usuario> usuarios);
        void RegistrarUsuarios(IEnumerable<Usuario> usuarios);
    }
}
