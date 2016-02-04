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
    public class ReceitasController : ApiController
    {
        private readonly IReceitasAppService _receitasApp;
        private readonly ICategoriaFinanceiraAppService _categoriaFinanceiraApp;

        public ReceitasController(IReceitasAppService receitasApp, ICategoriaFinanceiraAppService categoriaFinanceiraApp)
        {
            _receitasApp = receitasApp;
            _categoriaFinanceiraApp = categoriaFinanceiraApp;
        }

        [HttpGet]
        [Route("receitas")]
        //[GzipCompression]
        public async Task<HttpResponseMessage> Get()
        {
            var result = _receitasApp.GetReceitas();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("receitas")]
        public HttpResponseMessage Post(ReceitasViewModel receitasViewModel)
        {
            if (ModelState.IsValid)
            {
                _receitasApp.Add(receitasViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, receitasViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("receitas")]
        public HttpResponseMessage Put(ReceitasViewModel receitasViewModel)
        {
            if (ModelState.IsValid)
            {
                _receitasApp.Update(receitasViewModel);
                return Request.CreateResponse(HttpStatusCode.Created, "Update efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("receitas/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var receita = _receitasApp.GetById(id);

            if (receita == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Receita Não Encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, receita);
        }

        [HttpDelete]
        [Route("receitas/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _receitasApp.Remove(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }

        [HttpPost]
        [Route("receitas/search")]
        public async Task<HttpResponseMessage> Search(SearchReceitasViewModel searchViewModel)
        {
            if (searchViewModel == null)
            {
                var result = _receitasApp.GetReceitas();
                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return await tsc.Task;
            }
            else
            {
                var result = _receitasApp.SearchReceitas(searchViewModel);
                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return await tsc.Task;
            }
        }
    }
}