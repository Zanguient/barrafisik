using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.API.Controllers
{
    [RoutePrefix("api")]
    public class ClienteController : ApiController
    {
        private readonly IClienteAppService _clienteApp;

        public ClienteController(IClienteAppService clienteApp)
        {
            _clienteApp = clienteApp;
        }

        [HttpGet]
        [Route("clientes")]
        public async Task<HttpResponseMessage> GetClientes()
        {
            var result = _clienteApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("clientes")]
        public HttpResponseMessage Post(ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                var result = _clienteApp.Add(cliente);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return Request.CreateResponse(HttpStatusCode.Created, cliente);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}