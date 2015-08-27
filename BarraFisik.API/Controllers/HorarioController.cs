using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarraFisik.Application.Interfaces;
using BarraFisik.API.Filters;

namespace BarraFisik.API.Controllers
{
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
    }
}
