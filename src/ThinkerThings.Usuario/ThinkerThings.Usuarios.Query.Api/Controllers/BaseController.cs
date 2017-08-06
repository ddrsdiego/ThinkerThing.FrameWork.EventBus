using MediatR;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ThinkerThings.Usuarios.Command.Api.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected new HttpResponseMessage ResponseMessage { get; private set; }

        protected BaseController()
        {
            ResponseMessage = new HttpResponseMessage();
        }

        protected Task<HttpResponseMessage> CreateResponse(HttpStatusCode code, object result = null)
        {
            ResponseMessage = Request.CreateResponse(code, result);
            return Task.FromResult(ResponseMessage);
        }
    }
}