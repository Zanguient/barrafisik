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
    public class ProdutosController : ApiController
    {
        private readonly IProdutosAppService _produtosApp;

        public ProdutosController(IProdutosAppService produtosApp)
        {
            _produtosApp = produtosApp;
        }


        [HttpGet]
        [Route("produtos")]        
        public async Task<HttpResponseMessage> Get()
        {
            var result = _produtosApp.GetProdutos();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("produtos")]
        public HttpResponseMessage Post(ProdutosViewModel produtosViewModel)
        {
            if (ModelState.IsValid)
            {
                _produtosApp.Add(produtosViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, produtosViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("produtos")]
        public HttpResponseMessage Put(ProdutosViewModel produtosViewModel)
        {
            if (ModelState.IsValid)
            {
                _produtosApp.Update(produtosViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Produto atualizado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("produtos/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var produto = _produtosApp.GetById(id);

            if (produto == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Produto Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, produto);
        }

        [HttpGet]
        [Route("produtos/categoria/{idCategoria:Guid}")]
        public HttpResponseMessage GetByCategoria(Guid idCategoria)
        {
            var produtos = _produtosApp.GetByCategoria(idCategoria);

            if (produtos == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Categoria sem Produto");
            }

            return Request.CreateResponse(HttpStatusCode.OK, produtos);
        }

        [HttpDelete]
        [Route("produtos/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            try
            {
                _produtosApp.Remove(id);
            }
            catch (DbUpdateException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Este registro não pode ser removido.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }
    }
}
