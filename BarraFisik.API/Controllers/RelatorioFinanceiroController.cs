using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api")]
    public class RelatorioFinanceiroController : ApiController
    {
        private readonly IRelatorioFinanceiroAppService _relatorioFinanceiroApp;

        public RelatorioFinanceiroController(IRelatorioFinanceiroAppService relatorioFinanceiroApp)
        {
            _relatorioFinanceiroApp = relatorioFinanceiroApp;
        }


        [HttpPost]
        [Route("relatoriofinanceiro")]
        //[GzipCompression]
        public async Task<HttpResponseMessage> GetRelatorio(RelatorioFinanceiroSearchViewModel filters)
        {
            if (filters.DataInicio != null)                
                filters.DataInicio = new DateTime(filters.DataInicio.Value.Year, filters.DataInicio.Value.Month, filters.DataInicio.Value.Day, 01, 00, 00);

            if (filters.DataFim != null)
                filters.DataFim = new DateTime(filters.DataFim.Value.Year, filters.DataFim.Value.Month, filters.DataFim.Value.Day, 23, 59, 59);

            var result = _relatorioFinanceiroApp.GetRelatorio(filters);
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpGet]
        [Route("totalMeses")]
        //[GzipCompression]
        public async Task<HttpResponseMessage> GetTotalPorMes()
        {            
            var result = _relatorioFinanceiroApp.GetTotalPorMes();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

    }
}