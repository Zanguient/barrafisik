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
    public class CategoriaFinanceiraController : ApiController
    {
        private readonly ICategoriaFinanceiraAppService _categoriaFinanceiraApp;

        public CategoriaFinanceiraController(ICategoriaFinanceiraAppService categoriaFinanceiraApp)
        {
            _categoriaFinanceiraApp = categoriaFinanceiraApp;
        }

        [HttpGet]
        [Route("categoriafinanceira")]
        [GzipCompression]
        public async Task<HttpResponseMessage> Get()
        {
            var result = _categoriaFinanceiraApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("categoriafinanceira/{tipo}")]
        public async Task<HttpResponseMessage> GetByTipo(string tipo)
        {
            var result = _categoriaFinanceiraApp.GetByTipo(tipo);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("categoriafinanceira")]
        public HttpResponseMessage Post(CategoriaFinanceiraViewModel categoriaFinanceiraViewModel)
        {
            if (ModelState.IsValid)
            {
                _categoriaFinanceiraApp.Add(categoriaFinanceiraViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, categoriaFinanceiraViewModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        [Route("categoriafinanceira")]
        public HttpResponseMessage Put(CategoriaFinanceiraViewModel categoriaFinanceiraViewModel)
        {
            if (ModelState.IsValid)
            {
                _categoriaFinanceiraApp.Update(categoriaFinanceiraViewModel);

                return Request.CreateResponse(HttpStatusCode.Created, "Cadastro efetuado com sucesso!");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("categoriafinanceira/{id:Guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var fila = _categoriaFinanceiraApp.GetById(id);

            if (fila == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Categoria Não Encontrada");
            }

            return Request.CreateResponse(HttpStatusCode.OK, fila);
        }

        [HttpDelete]
        [Route("categoriafinanceira/{id:Guid}")]
        public HttpResponseMessage Remove(Guid id)
        {
            _categoriaFinanceiraApp.Remove(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Categoria excluída com sucesso!");
        }
    }
}