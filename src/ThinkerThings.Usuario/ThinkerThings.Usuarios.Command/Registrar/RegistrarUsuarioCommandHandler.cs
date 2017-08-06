using EventBus.Abstractions;
using MediatR;
using Rydo.Framework.MediatR.Handlres;
using Rydo.Framework.MediatR.Response;
using System;
using System.Collections.Generic;
using ThinkerThings.Dominio.Usuarios.Commands;
using ThinkerThings.Dominio.Usuarios.Contratos.Servicos;
using ThinkerThings.Dominio.Usuarios.Events;
using ThinkerThings.Dominio.Usuarios.Model;

namespace ThinkerThings.Usuarios.Command.Registrar
{
    public class RegistrarUsuarioCommandHandler : HandlerRequest<RegistrarUsuarioCommand, RegistrarUsuarioResponse>
    {
        private Usuario _novoUsuario;
        private readonly IEventBus _eventBus;
        private readonly IUsuarioServico _usuarioServico;

        public RegistrarUsuarioCommandHandler(IMediator mediator, IUsuarioServico usuarioServico, IEventBus eventBus)
            : base(mediator)
        {
            _eventBus = eventBus;
            _usuarioServico = usuarioServico;
        }

        public override RegistrarUsuarioResponse Handle(RegistrarUsuarioCommand message)
        {
            var resposta = message.Response;

            ValidarMensage(message, resposta);
            if (!resposta.Status.Sucesso)
                return resposta;

            VerificarUsuarioExiste(message, resposta);
            if (!resposta.Status.Sucesso)
                return resposta;

            _novoUsuario = new Usuario(message.Nome, message.Email, message.Login, message.Senha);

            _eventBus.Publish(new UsuarioCriadoEvent
            {
                Nome = _novoUsuario.Nome,
                Email = _novoUsuario.Email,
                DataCriacao = DateTime.Now
            });

            return resposta;
        }

        private static void ValidarMensage(RegistrarUsuarioCommand message, RegistrarUsuarioResponse response)
        {

        }

        private void VerificarUsuarioExiste(RegistrarUsuarioCommand message, RegistrarUsuarioResponse response)
        {
            if (_usuarioServico.VerficarUsuarioCadastrado(_novoUsuario))
                response.Status.AddReturnCode(new ReturnCode(1001, "Usuário já cadastrado com esse email."));
        }

        private void RegistrarNovoUsuario(RegistrarUsuarioCommand message, RegistrarUsuarioResponse response)
        {
            try
            {
                _usuarioServico.RegistrarUsuarios(new List<Usuario> { _novoUsuario });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
