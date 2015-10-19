using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface ICategoriaFinanceiraService : IServiceBase<CategoriaFinanceira>
    {
        IEnumerable<CategoriaFinanceira> GetByTipo(string tipo);
    }
}