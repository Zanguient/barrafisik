using System.Collections.Generic;
using AutoMapper;
using BarraFisik.Application.Interfaces;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Application.App
{
    public class RelatorioFinanceiroAppService : AppServiceBase<BarraFisikContext>, IRelatorioFinanceiroAppService
    {
        private readonly IRelatorioFinanceiroService _relatorioFinanceiroService;

        public RelatorioFinanceiroAppService(IRelatorioFinanceiroService relatorioFinanceiroService)
        {
            _relatorioFinanceiroService = relatorioFinanceiroService;
        }


        public IEnumerable<RelatorioFinanceiroViewModel> GetRelatorio(RelatorioFinanceiroSearchViewModel filters)
        {
            var filtros = Mapper.Map<RelatorioFinanceiroSearchViewModel, RelatorioFinanceiroSearch>(filters);

            return Mapper.Map<IEnumerable<RelatorioFinanceiro>, IEnumerable<RelatorioFinanceiroViewModel>>(
                    _relatorioFinanceiroService.GetRelatorio(filtros));
        }

        public IEnumerable<RelatorioFinanceiroViewModel> GetRelatorioReceitas(RelatorioFinanceiroSearchViewModel filters)
        {
            var filtros = Mapper.Map<RelatorioFinanceiroSearchViewModel, RelatorioFinanceiroSearch>(filters);

            return Mapper.Map<IEnumerable<RelatorioFinanceiro>, IEnumerable<RelatorioFinanceiroViewModel>>(
                    _relatorioFinanceiroService.GetRelatorioReceitas(filtros));
        }

        public IEnumerable<RelatorioFinanceiroViewModel> GetRelatorioDespesas(RelatorioFinanceiroSearchViewModel filters)
        {
            var filtros = Mapper.Map<RelatorioFinanceiroSearchViewModel, RelatorioFinanceiroSearch>(filters);

            return Mapper.Map<IEnumerable<RelatorioFinanceiro>, IEnumerable<RelatorioFinanceiroViewModel>>(
                    _relatorioFinanceiroService.GetRelatorioDespesas(filtros));
        }

        public IEnumerable<RelatorioFinanceiroTotalMesesViewModel> GetTotalPorMes()
        {
            return Mapper.Map<IEnumerable<RelatorioFinanceiroTotalMeses>, IEnumerable<RelatorioFinanceiroTotalMesesViewModel>>(
                    _relatorioFinanceiroService.GetTotalPorMes());
        }

        public void Dispose()
        {
            _relatorioFinanceiroService.Dispose();
        }
    }
}