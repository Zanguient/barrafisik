using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.API.Filters;

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
        [GzipCompression]
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

        [HttpPut]
        [Route("cliente/desativar/{id:Guid}")]
        public Task<HttpResponseMessage> DesativarCliente(Guid id)
        {
            var cliente = _clienteApp.GetById(id);

            var response = new HttpResponseMessage();

            if (cliente == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                cliente.IsAtivo = false;
                _clienteApp.Update(cliente);
                response = Request.CreateResponse(HttpStatusCode.OK, "Cliente Desativado");
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPut]
        [Route("cliente/ativar/{id:Guid}")]
        public Task<HttpResponseMessage> AtivarCliente(Guid id)
        {
            var cliente = _clienteApp.GetById(id);

            var response = new HttpResponseMessage();

            if (cliente == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                cliente.IsAtivo = true;
                _clienteApp.Update(cliente);
                response = Request.CreateResponse(HttpStatusCode.OK, "Cliente Desativado");
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}