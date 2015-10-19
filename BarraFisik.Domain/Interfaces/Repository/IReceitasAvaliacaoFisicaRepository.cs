using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IReceitasAvaliacaoFisicaRepository : IRepositoryBase<ReceitasAvaliacaoFisica>
    {
        IEnumerable<ReceitasAvaliacaoFisica> GetByCliente(Guid id);
    }
}