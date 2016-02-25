using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.API.Filters;
using System.Data.Entity.Infrastructure;
using WebApi.OutputCache.V2;

namespace BarraFisik.API.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class CategoriaFinanceiraController : ApiController
    {
        private readonly ICategoriaFinanceiraAppService _categoriaFinanceiraApp;

        public CategoriaFinanceiraController(ICategoriaFinanceiraAppService categoriaFinanceiraApp)
        {
            _categoriaFinanceiraApp = categoriaFinanceiraApp;
        }

        [HttpGet]
        [Route("categoriafinanceira")]
        [GzipCompression]
        [CacheOutput(ClientTimeSpan = 0, ServerTimeSpan = 28800)]
        public async Task<HttpResponseMessage> Get()
        {
            var result = _categoriaFinanceiraApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [InvalidateCacheOutput("Get")]
        [Route("categoriafinanceira/teste")]
        public HttpResponseMessage Teste()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Invalida Cache");
        }

        [HttpGet]
        [Route("categoriafinanceira/{tipo}")]
        public async Task<HttpResponseMessage> GetByTipo(string tipo)
        {
            var result = _categoriaFinanceiraApp.GetByTipo(tipo);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [InvalidateCacheOutput("Get")]
        [Route("categoriafinanceira")]
        public HttpResponseMessage Post(CategoriaFinanceiraViewModel categoriaFinanceiraViewModel)
        {
            if (ModelState.IsValid)
            {
                _categoriaFinanceiraApp.Add(categoriaFinanceiraViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, categoriaFinanceiraViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [InvalidateCacheOutput("Get")]
        [Route("categoriafinanceira")]
        public HttpResponseMessage Put(CategoriaFinanceiraViewModel categoriaFinanceiraViewModel)
        {
            if (ModelState.IsValid)
            {
                _categoriaFinanceiraApp.Update(categoriaFinanceiraViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Cadastro efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("categoriafinanceira/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var categoria = _categoriaFinanceiraApp.GetById(id);

            if (categoria == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Categoria Não Encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, categoria);
        }

        [HttpDelete]
        [InvalidateCacheOutput("Get")]
        [Route("categoriafinanceira/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            try
            {
                _categoriaFinanceiraApp.Remove(id);
            }
            catch (DbUpdateException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Este registro não pode ser removido.");
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, "Categoria excluída com sucesso!");
        }
    }
}