using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BarraFisik.API.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class MovimentacaoEstoqueController : ApiController
    {
        private readonly IMovimentacaoEstoqueAppService _movimentacaoApp;

        public MovimentacaoEstoqueController(IMovimentacaoEstoqueAppService movimentacaoApp)
        {
            _movimentacaoApp = movimentacaoApp;
        }


        [HttpGet]
        [Route("movimentacao")]        
        public async Task<HttpResponseMessage> Get()
        {
            var result = _movimentacaoApp.GetMovimentacoes();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("movimentacao/estoque/{id:Guid}")]
        public async Task<HttpResponseMessage> GetByEstoque(Guid id)
        {
            var result = _movimentacaoApp.GetByEstoque(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("movimentacao")]
        public HttpResponseMessage Post(MovimentacaoEstoqueViewModel movimentacaoViewModel)
        {
            if (ModelState.IsValid)
            {
                movimentacaoViewModel.TipoMovimento = "Entrada";
                _movimentacaoApp.Add(movimentacaoViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, movimentacaoViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("movimentacao")]
        public HttpResponseMessage Put(MovimentacaoEstoqueViewModel movimentacaoViewModel)
        {
            if (ModelState.IsValid)
            {
                bool verifica = _movimentacaoApp.Update(movimentacaoViewModel);
                return verifica ? Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!") : 
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Estoque insuficiente para esta operação!");
                
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("movimentacao/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var estoque = _movimentacaoApp.GetById(id);

            if (estoque == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Movimentação Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, estoque);
        }

        [HttpDelete]
        [Route("movimentacao/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            bool verifica = _movimentacaoApp.Remove(id);
            return verifica ? Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!") : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Movimentação maior que quantidade em Estoque!");
        }
    }
}
