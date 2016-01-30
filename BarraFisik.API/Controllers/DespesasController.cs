using System;
using System.ComponentModel.Design;
using System.Dynamic;
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
    public class DespesasController : ApiController
    {
        private readonly IDespesasAppService _despesasApp;

        public DespesasController(IDespesasAppService despesasApp)
        {
            _despesasApp = despesasApp;
        }

        [HttpGet]
        [Route("despesas")]
        [GzipCompression]
        public async Task<HttpResponseMessage> Get()
        {
            var result = _despesasApp.GetDespesas();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("despesas")]
        public HttpResponseMessage Post(DespesasViewModel despesasViewModel)
        {
            if (ModelState.IsValid)
            {
                _despesasApp.Add(despesasViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, despesasViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("despesas")]
        public HttpResponseMessage Put(DespesasViewModel despesasViewModel)
        {
            if (ModelState.IsValid)
            {
                _despesasApp.Update(despesasViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Cadastro efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("despesas/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var fila = _despesasApp.GetById(id);

            if (fila == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Despesa Não Encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, fila);
        }

        [HttpDelete]
        [Route("despesas/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _despesasApp.Remove(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }

        [HttpPost]
        [Route("despesas/search")]
        [GzipCompression]
        public async Task<HttpResponseMessage> Search(SearchDespesasViewModel searchViewModel)
        {           
            if (searchViewModel == null)
            {
                var result = _despesasApp.GetDespesasAll();
                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return await tsc.Task;
            } else {
                var result = _despesasApp.SearchDespesas(searchViewModel);
                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return await tsc.Task;
            }            
        }
    }
}