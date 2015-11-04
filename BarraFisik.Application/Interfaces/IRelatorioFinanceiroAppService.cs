using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IRelatorioFinanceiroAppService : IDisposable
    {
        IEnumerable<RelatorioFinanceiroViewModel> GetRelatorio(RelatorioFinanceiroSearchViewModel filters);
        IEnumerable<RelatorioFinanceiroTotalMesesViewModel> GetTotalPorMes();
    }
}