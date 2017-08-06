using System;
using System.Collections.Generic;
using ThinkerThings.Dominio.Usuarios.Contratos.Repositorios;
using ThinkerThings.Dominio.Usuarios.Contratos.Servicos;
using ThinkerThings.Dominio.Usuarios.Model;

namespace ThinkerThings.Dominio.Usuarios.Servico
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorioEscrita _usuarioRepositorioEscrita;

        public UsuarioServico(IUsuarioRepositorioEscrita usuarioRepositorioEscrita)
        {
            _usuarioRepositorioEscrita = usuarioRepositorioEscrita;
        }

        public void AlterarUsuarios(IEnumerable<Usuario> usuarios)
        {
            throw new NotImplementedException();
        }

        public void RegistrarUsuarios(IEnumerable<Usuario> usuarios)
        {
            try
            {
                _usuarioRepositorioEscrita.RegistrarUsuarios(usuarios);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerficarUsuarioCadastrado(Usuario usuario)
        {
            return false;
        }
    }
}
