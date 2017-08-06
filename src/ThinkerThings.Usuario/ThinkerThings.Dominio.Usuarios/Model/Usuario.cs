using CQRSlite.Domain;
using System;
using ThinkerThings.Dominio.Usuarios.Enum;

namespace ThinkerThings.Dominio.Usuarios.Model
{
    public class Usuario : AggregateRoot
    {
        protected Usuario()
        {

        }

        public Usuario(string nome, string email, string login, string senha)
        {
            Nome = nome;
            Login = login;
            Email = email;
            Senha = senha;
            DataCadastro = DateTime.Now;
            Status = StatusUsuario.PreCadastro;

        }

        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataAlteracao { get; private set; }
        public StatusUsuario Status { get; private set; }

        public void AlterarSenha(string novaSenha)
        {
            if (string.IsNullOrEmpty(novaSenha))
                throw new InvalidOperationException("Senha não deve ser nula ou vazia.");

            if (novaSenha.ToUpper().Equals(Senha.ToUpper()))
                throw new InvalidOperationException("Senha não deve ser diferente da atual.");

            Senha = novaSenha;
            DataAlteracao = DateTime.Now;
        }

        public void AlterarStatus(StatusUsuario novoStatus)
        {
            if (novoStatus == Status)
                throw new InvalidOperationException("Usuário já esta no status atual.");

            Status = novoStatus;
            DataAlteracao = DateTime.Now;

            //ApplyChange()
        }
    }
}
