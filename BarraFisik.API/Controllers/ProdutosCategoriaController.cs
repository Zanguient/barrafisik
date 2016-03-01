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
    public class ProdutosCategoriaController : ApiController
    {
        private readonly IProdutosCategoriaAppService _produtosCategoriaApp;

        public ProdutosCategoriaController(IProdutosCategoriaAppService produtosCategoriaApp)
        {
            _produtosCategoriaApp = produtosCategoriaApp;
        }


        [HttpGet]
        [Route("produtosCategoria")]        
        public async Task<HttpResponseMessage> Get()
        {
            var result = _produtosCategoriaApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("produtosCategoria")]
        public HttpResponseMessage Post(ProdutosCategoriaViewModel produtosCategoriaViewModel)
        {
            if (ModelState.IsValid)
            {
                _produtosCategoriaApp.Add(produtosCategoriaViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, produtosCategoriaViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("produtosCategoria")]
        public HttpResponseMessage Put(ProdutosCategoriaViewModel produtosCategoriaViewModel)
        {
            if (ModelState.IsValid)
            {
                _produtosCategoriaApp.Update(produtosCategoriaViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Categoria atualizado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("produtosCategoria/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var produtoCategoria = _produtosCategoriaApp.GetById(id);

            if (produtoCategoria == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Categoria Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, produtoCategoria);
        }

        [HttpDelete]
        [Route("produtosCategoria/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            try
            {
                _produtosCategoriaApp.Remove(id);
            }
            catch (DbUpdateException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Este registro não pode ser removido.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }
    }
}
