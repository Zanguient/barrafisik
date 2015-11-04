using System.Collections.Generic;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IRelatorioFinanceiroService : IServiceBase<RelatorioFinanceiro>
    {
        IEnumerable<RelatorioFinanceiro> GetRelatorio(RelatorioFinanceiroSearch filters);
        IEnumerable<RelatorioFinanceiroTotalMeses> GetTotalPorMes();
    }
}