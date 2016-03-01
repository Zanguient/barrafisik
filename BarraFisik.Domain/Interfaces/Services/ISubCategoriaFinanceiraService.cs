using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using System;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface ISubCategoriaFinanceiraService : IServiceBase<SubCategoriaFinanceira>
    {
        IEnumerable<SubCategoriaFinanceira> GetByCategoria(Guid idCategoria);
    }
}