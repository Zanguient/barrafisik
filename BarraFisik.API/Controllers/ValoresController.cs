using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.API.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class ValoresController : ApiController
    {
        private readonly IValoresAppService _valoresApp;

        public ValoresController(IValoresAppService valoresApp)
        {
            _valoresApp = valoresApp;
        }

        [HttpGet]
        [Route("valores")]
        public async Task<HttpResponseMessage> GetValores()
        {
            var result = _valoresApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("valores")]
        public HttpResponseMessage Post(ValoresViewModel valoresViewModel)
        {
            if (ModelState.IsValid)
            {
                _valoresApp.Add(valoresViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, valoresViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("valores")]
        public HttpResponseMessage Put(ValoresViewModel valoresViewModel)
        {
            if (ModelState.IsValid)
            {
                _valoresApp.Add(valoresViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Cadastro efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("valores/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var valores = _valoresApp.GetById(id);

            if (valores == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Valores não Encontrados");
            }

            return Request.CreateResponse(HttpStatusCode.OK, valores);
        }

        [HttpDelete]
        [Route("valores/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _valoresApp.Remove(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }

        [HttpGet]
        [Route("valores/cliente")]
        public HttpResponseMessage GetValorCliente(int qtdDias, int horario)
        {
            var valores = _valoresApp.GetValorCliente(qtdDias, horario);

            if (valores == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Valores não Encontrados");
            }

            return Request.CreateResponse(HttpStatusCode.OK, valores);
        }
    }
}