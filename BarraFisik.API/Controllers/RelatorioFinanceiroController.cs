using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.API.Filters;
using BarraFisik.API.Models;

namespace BarraFisik.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api")]
    public class RelatorioFinanceiroController : ApiController
    {
        private readonly IRelatorioFinanceiroAppService _relatorioFinanceiroApp;
        private readonly ITipoPagamentoAppService _tipoPagamentoApp;

        public RelatorioFinanceiroController(IRelatorioFinanceiroAppService relatorioFinanceiroApp, ITipoPagamentoAppService tipoPagamentoApp)
        {
            _relatorioFinanceiroApp = relatorioFinanceiroApp;
            _tipoPagamentoApp = tipoPagamentoApp;
        }


        [HttpPost]
        [AllowAnonymous]       
        [Route("relatoriofinanceiro")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetRelatorio(RelatorioFinanceiroSearchViewModel filters)
        {
            //if (filters.DataInicio != null)                
            //    filters.DataInicio = new DateTime(filters.DataInicio.Value.Year, filters.DataInicio.Value.Month, filters.DataInicio.Value.Day, 01, 00, 00);

            //if (filters.DataFim != null)
            //    filters.DataFim = new DateTime(filters.DataFim.Value.Year, filters.DataFim.Value.Month, filters.DataFim.Value.Day, 23, 59, 59);

            //var result = _relatorioFinanceiroApp.GetRelatorio(filters);

            var list = _relatorioFinanceiroApp.GetRelatorio(filters);

            var total = new Dictionary<string, decimal>();
            foreach (var item in _tipoPagamentoApp.GetAll())
            {
                var listByTipo = list.Where(e => e.TipoPagamento == item.Descricao);
                total.Add(item.Descricao, listByTipo.Sum(e => e.ValorTotal));
            }            

            var relatorioContainer = new RelatorioContainer()
            {
                ListRelatorio = list,
                TotalByTipoPagamento = total
            };
            
            var response = Request.CreateResponse(HttpStatusCode.OK, relatorioContainer);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        
         

        [HttpPost]
        [Route("relatoriofinanceiro/receitas")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetRelatorioReceitas(RelatorioFinanceiroSearchViewModel filters)
        {
            //var result = _relatorioFinanceiroApp.GetRelatorioReceitas(filters);

            var list = _relatorioFinanceiroApp.GetRelatorioReceitas(filters);

            var total = new Dictionary<string, decimal>();
            foreach (var item in _tipoPagamentoApp.GetAll())
            {
                var listByTipo = list.Where(e => e.TipoPagamento == item.Descricao);
                total.Add(item.Descricao, listByTipo.Sum(e => e.ValorTotal));
            }

            var relatorioContainer = new RelatorioContainer()
            {
                ListRelatorio = list,
                TotalByTipoPagamento = total
            };

            var response = Request.CreateResponse(HttpStatusCode.OK, relatorioContainer);

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }

        [HttpPost]
        [Route("relatoriofinanceiro/despesas")]
        [GzipCompression]
        public async Task<HttpResponseMessage> GetRelatorioDespesas(RelatorioFinanceiroSearchViewModel filters)
        {
            //var result = _relatorioFinanceiroApp.GetRelatorioDespesas(filters);
            //var response = Request.CreateResponse(HttpStatusCode.OK, result);

            var list = _relatorioFinanceiroApp.GetRelatorioDespesas(filters);

            var total = new Dictionary<string, decimal>();
            foreach (var item in _tipoPagamentoApp.GetAll())
            {
                var listByTipo = list.Where(e => e.TipoPagamento == item.Descricao);
                total.Add(item.Descricao, listByTipo.Sum(e => e.ValorTotal));
            }

            var relatorioContainer = new RelatorioContainer()
            {
                ListRelatorio = list,
                TotalByTipoPagamento = total
            };

            var response = Request.CreateResponse(HttpStatusCode.OK, relatorioContainer);

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

        [HttpGet]
        [AllowAnonymous]
        [Route("relatorioFinanceiro/totalByTipoPagamento")]
        public HttpResponseMessage TotalByTipoPagamento()
        {
            IDictionary<string, decimal> total = new Dictionary<string, decimal>();
            foreach (var item in _tipoPagamentoApp.GetAll())
            {
                total.Add(item.Descricao, _relatorioFinanceiroApp.GetTotalByTipoPagamento(item.TipoPagamentoId));
            }

            return Request.CreateResponse(HttpStatusCode.OK, total);

        }

    }
}