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
    [Authorize]
    [RoutePrefix("api")]
    public class MensalidadesController : ApiController
    {
        private readonly IMensalidadesAppService _mensalidadesApp;

        public MensalidadesController(IMensalidadesAppService mensalidadesApp)
        {
            _mensalidadesApp = mensalidadesApp;
        }

        [HttpGet]
        [Route("mensalidades/{id:Guid}")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetMensalidades(Guid id)
        {
            var result = _mensalidadesApp.GetMensalidadesCliente(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("mensalidades")]
        public HttpResponseMessage Post(MensalidadesViewModel mensalidade)
        {
            if (ModelState.IsValid)
            {
                var result = _mensalidadesApp.AdicionarMensalidade(mensalidade);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return Request.CreateResponse(HttpStatusCode.Created, mensalidade);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("mensalidades")]
        public HttpResponseMessage Put(MensalidadesViewModel mensalidade)
        {
            if (ModelState.IsValid)
            {
                var result = _mensalidadesApp.Update(mensalidade);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Erros)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return Request.CreateResponse(HttpStatusCode.Created, mensalidade);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpDelete]
        [Route("mensalidades/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _mensalidadesApp.Remove(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }
    }
}