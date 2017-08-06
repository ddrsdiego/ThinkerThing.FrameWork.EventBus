using MediatR;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ThinkerThings.Usuarios.Command.Registrar;

namespace ThinkerThings.Usuarios.Command.Api.Controllers
{
    [RoutePrefix("v1")]
    public class UsuariosController : ApiController
    {
        private readonly IMediator _mediator;

        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> RegistrarUsuario([FromBody] RegistrarUsuarioCommand command)
        {
            return await Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, _mediator.Send(command)));
        }
    }
}
