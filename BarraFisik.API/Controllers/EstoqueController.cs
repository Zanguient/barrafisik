using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BarraFisik.API.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class EstoqueController : ApiController
    {
        private readonly IEstoqueAppService _estoqueApp;

        public EstoqueController(IEstoqueAppService estoqueApp)
        {
            _estoqueApp = estoqueApp;
        }

        [HttpGet]
        [Route("estoque")]        
        public async Task<HttpResponseMessage> Get()
        {
            var result = _estoqueApp.GetEstoques();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("estoque/armazem/{id:Guid}")]
        public async Task<HttpResponseMessage> GetByArmazem(Guid id)
        {
            var result = _estoqueApp.GetByArmazem(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("estoque")]
        public HttpResponseMessage Post(EstoqueViewModel estoqueViewModel)
        {            
            if (ModelState.IsValid)
            {
                if (!(_estoqueApp.ExisteEstoque(estoqueViewModel.ArmazemId, estoqueViewModel.ProdutoId)))
                {
                    _estoqueApp.Add(estoqueViewModel);
                    return Request.CreateResponse(HttpStatusCode.Created, estoqueViewModel);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Este produto já está vinculado a este armazém!");
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("estoque")]
        public HttpResponseMessage Put(EstoqueViewModel estoqueViewModel)
        {
            if (ModelState.IsValid)
            {
                _estoqueApp.Update(estoqueViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Estoque atualizado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("estoque/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var estoque = _estoqueApp.GetById(id);

            if (estoque == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Estoque Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, estoque);
        }

        [HttpDelete]
        [Route("estoque/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            try
            {
                _estoqueApp.Remove(id);
            }
            catch (DbUpdateException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Este registro não pode ser removido. Existem vendas deste produto.");
            }
            

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }
    }
}
