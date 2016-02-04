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
    public class FornecedoresController : ApiController
    {
        private readonly IFornecedoresAppService _fornecedoresApp;

        public FornecedoresController(IFornecedoresAppService fornecedoresApp)
        {
            _fornecedoresApp = fornecedoresApp;
        }

        [HttpGet]
        [Route("fornecedores")]        
        public async Task<HttpResponseMessage> Get()
        {
            var result = _fornecedoresApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("fornecedoresAtivos")]
        public async Task<HttpResponseMessage> GetAtivos()
        {
            var result = _fornecedoresApp.GetAllAtivos();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("fornecedores")]
        public HttpResponseMessage Post(FornecedoresViewModel fornecedoresViewModel)
        {
            if (ModelState.IsValid)
            {
                _fornecedoresApp.Add(fornecedoresViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, fornecedoresViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("fornecedores")]
        public HttpResponseMessage Put(FornecedoresViewModel fornecedoresViewModel)
        {
            if (ModelState.IsValid)
            {
                _fornecedoresApp.Update(fornecedoresViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Fornecedor atualizado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("fornecedores/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var fornecedor = _fornecedoresApp.GetById(id);

            if (fornecedor == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Fornecedor Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, fornecedor);
        }

        [HttpDelete]
        [Route("fornecedores/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            try
            {
                _fornecedoresApp.Remove(id);
            }
            catch (DbUpdateException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Este registro não pode ser removido.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }
    }
}
