using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.ValueObjects;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class RelatorioFinanceiroRepository : RepositoryBase<RelatorioFinanceiro, BarraFisikContext>,
        IRelatorioFinanceiroRepository
    {
    }
}