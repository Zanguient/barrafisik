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
    public class VendasController : ApiController
    {
        private readonly IVendasAppService _vendasApp;

        public VendasController(IVendasAppService vendasApp)
        {
            _vendasApp = vendasApp;
        }

        [HttpGet]
        [GzipCompression]
        [Route("vendas")]        
        public async Task<HttpResponseMessage> Get()
        {
            var result = _vendasApp.GetVendas();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [GzipCompression]
        [Route("vendas/pendentes/{mes:int}/{ano:int}")]
        public async Task<HttpResponseMessage> GetPendentes(int mes, int ano)
        {
            var result = _vendasApp.GetPendentes(mes, ano);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        //[GzipCompression]
        [Route("vendas/anual/{ano:int}")]
        public async Task<HttpResponseMessage> GetVendasAnual(int ano)
        {
            var result = _vendasApp.GetVendasAnual( ano);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [GzipCompression]
        [Route("vendas/cliente/{idCliente:Guid}")]
        public async Task<HttpResponseMessage> GetVendasByCliente(Guid idCliente)
        {
            var result = _vendasApp.GetVendasByCliente(idCliente);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("vendas")]
        public HttpResponseMessage Post(VendasViewModel vendasViewModel)
        {
            if (ModelState.IsValid)
            {
                _vendasApp.Add(vendasViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, vendasViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("vendas")]
        public HttpResponseMessage Put(VendasViewModel vendasViewModel)
        {
            if (ModelState.IsValid)
            {
                _vendasApp.Update(vendasViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Venda atualizada com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("vendas/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var venda = _vendasApp.GetById(id);

            if (venda == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Venda Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, venda);
        }

        [HttpDelete]
        [Route("vendas/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _vendasApp.Remove(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }

        [HttpPost]
        [Route("vendas/search")]
        public async Task<HttpResponseMessage> Search(SearchVendasViewModel searchViewModel)
        {
            if (searchViewModel == null)
            {
                var result = _vendasApp.GetVendas();
                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return await tsc.Task;
            }
            else
            {
                var result = _vendasApp.SearchVendas(searchViewModel);
                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return await tsc.Task;
            }
        }
    }
}
