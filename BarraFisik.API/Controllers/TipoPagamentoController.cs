using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BarraFisik.API.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class TipoPagamentoController : ApiController
    {
        private readonly ITipoPagamentoAppService _tipoPagamentoApp;

        public TipoPagamentoController(ITipoPagamentoAppService tipoPagamentoAppService)
        {
            _tipoPagamentoApp = tipoPagamentoAppService;
        }

        [HttpGet]
        [Route("tipo")]        
        public async Task<HttpResponseMessage> GetTipos()
        {
            var result = _tipoPagamentoApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("tipo")]
        public HttpResponseMessage Post(TipoPagamentoViewModel tipoPagamentoViewModel)
        {
            if (ModelState.IsValid)
            {
                _tipoPagamentoApp.Add(tipoPagamentoViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, tipoPagamentoViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("tipo")]
        public HttpResponseMessage Put(TipoPagamentoViewModel tipoPagamentoViewModel)
        {
            if (ModelState.IsValid)
            {
                _tipoPagamentoApp.Update(tipoPagamentoViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Cadastro efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("tipo/{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            var tipo = _tipoPagamentoApp.GetById(id);

            if (tipo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Tipo Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, tipo);
        }

        [HttpDelete]
        [Route("tipo/{id:int}")]
        public HttpResponseMessage Remove(int id)
        {
            try
            {
                _tipoPagamentoApp.Remove(id);
            }
            catch (DbUpdateException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Este registro não pode ser removido.");
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }
    }
}
