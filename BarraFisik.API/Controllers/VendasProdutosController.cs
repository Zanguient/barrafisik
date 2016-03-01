using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarraFisik.API.Filters;

namespace BarraFisik.API.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class VendasProdutosController : ApiController
    {
        private readonly IVendasProdutosAppService _vendasProdutosApp;

        public VendasProdutosController(IVendasProdutosAppService vendasProdutosApp)
        {
            _vendasProdutosApp = vendasProdutosApp;
        }


        [HttpGet]
        [GzipCompression]
        [Route("vendasprodutos")]        
        public async Task<HttpResponseMessage> Get()
        {
            var result = _vendasProdutosApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [GzipCompression]
        [Route("vendasprodutos/venda/{vendaId:Guid}")]
        public async Task<HttpResponseMessage> GetByVenda(Guid vendaId)
        {
            var result = _vendasProdutosApp.GetByVenda(vendaId);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("vendasprodutos/addproduto")]
        public async Task<HttpResponseMessage> AddProduto(VendasProdutosViewModel vendasProdutosViewModel)
        {
            _vendasProdutosApp.AddProduto(vendasProdutosViewModel);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Produtos Adicionados");

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("vendasprodutos/{idVenda:Guid}")]
        public async Task<HttpResponseMessage> Post(IEnumerable<VendasProdutosViewModel> vendasProdutosList, Guid idVenda)
        {
            _vendasProdutosApp.AddVendasProdutos(vendasProdutosList, idVenda);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Produtos Adicionados");

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpDelete]
        [Route("vendasprodutos/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _vendasProdutosApp.Remove(id);            

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }

    }
}
