using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IEstoqueRepository : IRepositoryBase<Estoque>
    {
        IEnumerable<Estoque> GetEstoques();
        IEnumerable<Estoque> GetByArmazem(Guid id);
    }
}