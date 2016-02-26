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
    public class ArmazemController : ApiController
    {
        private readonly IArmazemAppService _armazemApp;

        public ArmazemController(IArmazemAppService armazemApp)
        {
            _armazemApp = armazemApp;
        }

        [HttpGet]
        [Route("armazem")]        
        public async Task<HttpResponseMessage> Get()
        {
            var result = _armazemApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("armazem")]
        public HttpResponseMessage Post(ArmazemViewModel armazemViewModel)
        {
            if (ModelState.IsValid)
            {
                _armazemApp.Add(armazemViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, armazemViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("armazem")]
        public HttpResponseMessage Put(ArmazemViewModel armazemViewModel)
        {
            if (ModelState.IsValid)
            {
                _armazemApp.Update(armazemViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Armazém atualizado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("armazem/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var armazem = _armazemApp.GetById(id);

            if (armazem == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Armazém Não Encontrado");
            }

            return Request.CreateResponse(HttpStatusCode.OK, armazem);
        }

        [HttpDelete]
        [Route("armazem/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            try
            {
                _armazemApp.Remove(id);
            }
            catch (DbUpdateException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Este registro não pode ser removido.");
            }
            

            return Request.CreateResponse(HttpStatusCode.OK, "Dado excluído com sucesso!");
        }
    }
}
