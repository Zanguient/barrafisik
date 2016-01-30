using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IDespesasService : IServiceBase<Despesas>
    {
        IEnumerable<Despesas> GetDespesas();
        IEnumerable<Despesas> SearchDespesas(SearchDespesa sd);
        IEnumerable<Despesas> GetDespesasAll();
    }
}