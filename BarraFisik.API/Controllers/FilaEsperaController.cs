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
    public class FilaEsperaController : ApiController
    {
        private readonly IFilaEsperaAppService _filaEsperaApp;

        public FilaEsperaController(IFilaEsperaAppService filaEsperaApp)
        {
            _filaEsperaApp = filaEsperaApp;
        }

        [HttpGet]
        [Route("filaespera")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetFila()
        {
            var result = _filaEsperaApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("filaespera")]
        public HttpResponseMessage Post(FilaEsperaViewModel filaEsperaViewModel)
        {
            if (ModelState.IsValid)
            {
                _filaEsperaApp.Add(filaEsperaViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, filaEsperaViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("filaespera")]
        public HttpResponseMessage Put(FilaEsperaViewModel filaEsperaViewModel)
        {
            if (ModelState.IsValid)
            {
                _filaEsperaApp.Add(filaEsperaViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Cadastro efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("filaespera/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var fila = _filaEsperaApp.GetById(id);

            if (fila == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Fila Não Encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, fila);
        }

        [HttpDelete]
        [Route("filaespera/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _filaEsperaApp.Remove(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }
    }
}