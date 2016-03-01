using System;
using System.Collections.Generic;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;
using System.Data.Entity;

namespace BarraFisik.Infra.Data.Repository
{
    public class SubCategoriaFinanceiraRepository : RepositoryBase<SubCategoriaFinanceira, BarraFisikContext>, ISubCategoriaFinanceiraRepository
    {
        public IEnumerable<SubCategoriaFinanceira> GetByCategoria(Guid idCategoria)
        {
            return DbSet.Include("CategoriaFinanceira").Where(x => x.CategoriaFinanceiraId == idCategoria).ToList();
        }
    }
}