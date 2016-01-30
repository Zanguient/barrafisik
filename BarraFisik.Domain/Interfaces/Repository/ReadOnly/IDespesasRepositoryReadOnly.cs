using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IDespesasRepositoryReadOnly
    {
        IEnumerable<Despesas> GetDespesas();
        IEnumerable<Despesas> SearchDespesas(SearchDespesa sd);
    }
}