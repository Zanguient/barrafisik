using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IReceitasAvaliacaoFisicaService : IServiceBase<ReceitasAvaliacaoFisica>
    {
        IEnumerable<ReceitasAvaliacaoFisica> GetByCliente(Guid id);
    }
}