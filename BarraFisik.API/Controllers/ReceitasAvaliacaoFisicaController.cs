using System;
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
    public class ReceitasAvaliacaoFisicaController : ApiController
    {
        private readonly IReceitasAvaliacaoFisicaAppService _receitasAvaliacaoFisicaApp;

        public ReceitasAvaliacaoFisicaController(IReceitasAvaliacaoFisicaAppService receitasAvaliacaoFisicaApp)
        {
            _receitasAvaliacaoFisicaApp = receitasAvaliacaoFisicaApp;
        }


        [HttpGet]
        [Route("receitasAvaliacaoFisica")]
        [GzipCompression]
        public async Task<HttpResponseMessage> Get()
        {
            var result = _receitasAvaliacaoFisicaApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("receitasAvaliacaoPorCliente/{id:Guid}")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetAllPorCliente(Guid id)
        {
            var result = _receitasAvaliacaoFisicaApp.GetByCliente(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }


        [HttpPost]
        [Route("receitasAvaliacaoFisica")]
        public HttpResponseMessage Post(ReceitasAvaliacaoFisicaViewModel receitasAvaliacaoFisicaViewModel)
        {            
            receitasAvaliacaoFisicaViewModel.Nome = "Avaliação Fisica";
            receitasAvaliacaoFisicaViewModel.CategoriaFinanceiraId = new Guid("1c1278df-f5a5-4407-a0c4-bdbb71c362b1");
            //receitasAvaliacaoFisicaViewModel.DataPagamento = DateTime.Now;
            if (ModelState.IsValid)
            {
                _receitasAvaliacaoFisicaApp.Add(receitasAvaliacaoFisicaViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, receitasAvaliacaoFisicaViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("receitasAvaliacaoFisica")]
        public HttpResponseMessage Put(ReceitasAvaliacaoFisicaViewModel receitasAvaliacaoFisicaViewModel)
        {
            if (ModelState.IsValid)
            {
                _receitasAvaliacaoFisicaApp.Update(receitasAvaliacaoFisicaViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Cadastro efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("receitasAvaliacaoFisica/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var receitasAvaliacao = _receitasAvaliacaoFisicaApp.GetById(id);

            if (receitasAvaliacao == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Receita Avaliação Física Não Encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, receitasAvaliacao);
        }

        [HttpDelete]
        [Route("receitasAvaliacaoFisica/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _receitasAvaliacaoFisicaApp.Remove(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }
    }
}