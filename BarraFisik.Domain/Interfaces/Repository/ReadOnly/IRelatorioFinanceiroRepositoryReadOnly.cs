using System.Collections.Generic;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IRelatorioFinanceiroRepositoryReadOnly
    {
        IEnumerable<RelatorioFinanceiro> GetRelatorio(RelatorioFinanceiroSearch fitlers);
        IEnumerable<RelatorioFinanceiroTotalMeses> GetTotalPorMes();
    }
}