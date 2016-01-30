using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IDespesasRepository : IRepositoryBase<Despesas>
    {
        IEnumerable<Despesas> GetDespesasAll();
    }
}