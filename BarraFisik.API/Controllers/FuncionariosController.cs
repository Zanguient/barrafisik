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
    public class FuncionariosController : ApiController
    {
        private readonly IFuncionariosAppService _funcionariosApp;

        public FuncionariosController(IFuncionariosAppService funcionariosApp)
        {
            _funcionariosApp = funcionariosApp;
        }

        [HttpGet]
        [Route("funcionarios")]        
        public async Task<HttpResponseMessage> Get()
        {
            var result = _funcionariosApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("funcionariosAtivos")]
        public async Task<HttpResponseMessage> GetAtivos()
        {
            var result = _funcionariosApp.GetAllAtivos();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("funcionarios")]
        public HttpResponseMessage Post(FuncionariosViewModel funcionariosViewModel)
        {
            if (ModelState.IsValid)
            {
                _funcionariosApp.Add(funcionariosViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, funcionariosViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("funcionarios")]
        public HttpResponseMessage Put(FuncionariosViewModel funcionariosViewModel)
        {
            if (ModelState.IsValid)
            {
                _funcionariosApp.Update(funcionariosViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Funcionário atualizado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("funcionarios/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var funcionario = _funcionariosApp.GetById(id);

            if (funcionario == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Funcionário Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, funcionario);
        }

        [HttpDelete]
        [Route("funcionarios/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            try
            {
                _funcionariosApp.Remove(id);
            }
            catch (DbUpdateException ex)
            {                                
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Este registro não pode ser removido.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");

        }
    }
}
