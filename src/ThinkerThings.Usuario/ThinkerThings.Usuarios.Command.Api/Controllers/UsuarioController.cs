using MediatR;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ThinkerThings.Dominio.Usuarios.Commands;
using ThinkerThings.Usuarios.Command.Api.Helpers;

namespace ThinkerThings.Usuarios.Command.Api.Controllers
{
    [RoutePrefix("api/v1/usuarios")]
    public class UsuarioController : BaseController
    {
        public UsuarioController(IMediator mediator)
            : base(mediator)
        {

        }

        [HttpGet]
        [Route("{id}")]
        public Task<HttpResponseMessage> Obter([FromUri] int id)
        {
            var response = new HttpResponseMessage();

            try
            {
                var responseMessage = new { };
            }
            catch (Exception)
            {

            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);

            return tsc.Task;
        }

        [HttpPost]
        [Route("")]
        public Task<HttpResponseMessage> Registrar([FromBody]RegistrarUsuarioCommand command)
        {
            try
            {
                return CreateResponseMessageSuccess(_mediator.Send(command).Result);
            }
            catch (Exception)
            {
                return CreateResponseMessageError(_mediator.Send(command).Result);
            }
        }

        [HttpPost]
        [Route("{id}")]
        public Task<HttpResponseMessage> AtualizarUsuario([FromUri] int id, [FromBody]AtualizarUsuarioCommand command)
        {
            var response = new HttpResponseMessage();

            try
            {
                var responseMessage = _mediator.Send(command).Result;
            }
            catch (Exception)
            {
                //response = Request.CreateResponse(HttpStatusCode.OK, new { code = (int)HttpStatusCode.BadRequest, status = MESSAGE_ERROR, data = new { message = ex.Message } });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);

            return tsc.Task;
        }

        /// <summary>
        /// api/v1/usuarios/1/resetpassword
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/resetpassword")]
        public Task<HttpResponseMessage> ResetSenhaUsuario([FromUri] int id, [FromBody]ResetSenhaUsuarioCommand command)
        {
            var response = new HttpResponseMessage();

            try
            {
                command.IdUsuario = id;
                return CreateResponseMessageSuccess(_mediator.Send(command).Result);
            }
            catch (Exception ex)
            {
                //response = Request.CreateResponse(HttpStatusCode.OK, new { code = (int)HttpStatusCode.BadRequest, status = MESSAGE_ERROR, data = new { message = ex.Message } });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);

            return tsc.Task;
        }
    }
}