using System.Collections.Generic;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class CategoriaFinanceiraRepository : RepositoryBase<CategoriaFinanceira, BarraFisikContext>, ICategoriaFinanceiraRepository
    {
        public IEnumerable<CategoriaFinanceira> GetByTipo(string tipo)
        {
            return DbSet.Where(r => r.Tipo == tipo).ToList();
        }
    }
}