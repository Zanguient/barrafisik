using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IDespesasService : IServiceBase<Despesas>
    {
        IEnumerable<Despesas> GetDespesas();
    }
}