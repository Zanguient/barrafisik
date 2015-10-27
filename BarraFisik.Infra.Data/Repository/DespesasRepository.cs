using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class DespesasRepository : RepositoryBase<Despesas, BarraFisikContext>, IDespesasRepository
    {
        public IEnumerable<Despesas> GetDespesas()
        {            
            return DbSet.Include("CategoriaFinanceira").ToList();
        }
    }
}