using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface ISubCategoriaFinanceiraRepository : IRepositoryBase<SubCategoriaFinanceira>
    {
        IEnumerable<SubCategoriaFinanceira> GetByCategoria(Guid idCategoria); 
    }
}