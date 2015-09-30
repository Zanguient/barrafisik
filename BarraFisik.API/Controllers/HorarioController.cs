using System;
using System.Collections.Generic;
using System.Linq;
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
    public class HorarioController : ApiController
    {
        private readonly IHorarioAppService _horarioApp;

        public HorarioController(IHorarioAppService horarioApp)
        {
            _horarioApp = horarioApp;
        }

        [HttpGet]
        [Route("horarios")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetHorarios()
        {
            var result = _horarioApp.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("horarios/total")]
        public async Task<HttpResponseMessage> GetTotalHorarios()
        {
            var result = _horarioApp.GetHorarios();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("horarios")]
        public async Task<HttpResponseMessage> Post(HorarioViewModel horario)
        {
            _horarioApp.Add(horario);
            var response = Request.CreateResponse(HttpStatusCode.OK, horario);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("horarios/cliente/{id:Guid}")]
        public async Task<HttpResponseMessage> GetHorarioCliente(Guid id)
        {
            var result = _horarioApp.GetHorarioCliente(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPut]
        [Route("horarios")]
        public HttpResponseMessage Put(HorarioViewModel horario)
        {
            _horarioApp.Add(horario);
            return Request.CreateResponse(HttpStatusCode.OK, "Horario Salvo com Sucesso");
        }
    }
}
