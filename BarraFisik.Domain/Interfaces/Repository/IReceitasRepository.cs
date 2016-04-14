using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IReceitasRepository : IRepositoryBase<Receitas>
    {
        IEnumerable<Receitas> GetReceitas();
    }
}