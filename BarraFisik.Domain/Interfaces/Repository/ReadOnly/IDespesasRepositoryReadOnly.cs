using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IDespesasRepositoryReadOnly
    {
        IEnumerable<Despesas> GetDespesas();
    }
}