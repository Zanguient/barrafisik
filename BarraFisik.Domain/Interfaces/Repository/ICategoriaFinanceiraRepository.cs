using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface ICategoriaFinanceiraRepository : IRepositoryBase<CategoriaFinanceira>
    {
        IEnumerable<CategoriaFinanceira> GetByTipo(string tipo);
    }
}