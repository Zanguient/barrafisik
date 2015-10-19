using System;
using System.Collections.Generic;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class ReceitasAvaliacaoFisicaRepository : RepositoryBase<ReceitasAvaliacaoFisica, BarraFisikContext>, IReceitasAvaliacaoFisicaRepository
    {
        public IEnumerable<ReceitasAvaliacaoFisica> GetByCliente(Guid id)
        {
            return base.DbSet.Where(r => r.ClienteId == id);
        }
    }
}