using MediatR;
using Rydo.Framework.MediatR.Response;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ThinkerThings.Usuarios.Command.Api.Helpers
{
    public class BaseController : ApiController
    {
        private const string MESSAGE_ERROR = "error";
        private const string MESSAGE_SUCCESS = "success";
        private HttpResponseMessage _response;

        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new HttpResponseMessage();
        }

        protected Task<HttpResponseMessage> CreateResponseMessageSuccess(ResponseMessage responseMessage)
        {
            var ret = new HttpResponseMessage();

            if (responseMessage.Status.Sucesso)
                _response = Request.CreateResponse(HttpStatusCode.OK, new { code = (int)HttpStatusCode.OK, status = MESSAGE_SUCCESS, data = responseMessage });
            else
                _response = Request.CreateResponse(HttpStatusCode.OK, new { code = (int)HttpStatusCode.OK, status = MESSAGE_ERROR, data = responseMessage });

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(_response);

            return tsc.Task;
        }

        protected Task<HttpResponseMessage> CreateResponseMessageError(ResponseMessage responseMessage)
        {
            var ret = new HttpResponseMessage();

            if (responseMessage.Status.Sucesso)
                _response = Request.CreateResponse(HttpStatusCode.OK, new { code = (int)HttpStatusCode.OK, status = MESSAGE_SUCCESS, data = responseMessage });
            else
                _response = Request.CreateResponse(HttpStatusCode.OK, new { code = (int)HttpStatusCode.OK, status = MESSAGE_ERROR, data = responseMessage });

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(_response);

            return tsc.Task;
        }
    }

    public class ResultMessage
    {
        public ResultMessage(HttpStatusCode httpStatusCode, dynamic data)
        {
            Data = data;
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; set; }
        public dynamic Data { get; set; }
    }
}