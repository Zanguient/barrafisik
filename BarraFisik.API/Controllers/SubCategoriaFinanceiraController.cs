using System;
using System.Data.Entity.Infrastructure;
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
    public class SubCategoriaFinanceiraController : ApiController
    {
        private readonly ISubCategoriaFinanceiraAppService _subCategoriaFinanceiraApp;

        public SubCategoriaFinanceiraController(ISubCategoriaFinanceiraAppService subCategoriaFinanceiraApp)
        {
            _subCategoriaFinanceiraApp = subCategoriaFinanceiraApp;
        }

        [HttpGet]
        [Route("subCategoriaFinanceira")]
        [GzipCompression]
        public async Task<HttpResponseMessage> Get()
        {
            var result = _subCategoriaFinanceiraApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("subCategoriaFinanceira/Categoria/{idCategoria:Guid}")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetByCategoria(Guid idCategoria)
        {
            var result = _subCategoriaFinanceiraApp.GetByCategoria(idCategoria);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("subCategoriaFinanceira")]
        public HttpResponseMessage Post(SubCategoriaFinanceiraViewModel subCategoriaFinanceiraViewModel)
        {
            if (ModelState.IsValid)
            {
                _subCategoriaFinanceiraApp.Add(subCategoriaFinanceiraViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, subCategoriaFinanceiraViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("subCategoriaFinanceira")]
        public HttpResponseMessage Put(SubCategoriaFinanceiraViewModel subCategoriaFinanceiraViewModel)
        {
            if (ModelState.IsValid)
            {
                _subCategoriaFinanceiraApp.Update(subCategoriaFinanceiraViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Cadastro efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("subCategoriaFinanceira/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var subcat = _subCategoriaFinanceiraApp.GetById(id);

            if (subcat == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "SubCategoria Não Encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, subcat);
        }

        [HttpDelete]
        [Route("subCategoriaFinanceira/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            try
            {
                _subCategoriaFinanceiraApp.Remove(id);
            }
            catch (DbUpdateException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Este registro não pode ser removido.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Categoria excluída com sucesso!");
        }
    }
}