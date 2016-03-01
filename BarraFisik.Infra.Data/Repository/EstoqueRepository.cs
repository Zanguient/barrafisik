using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class EstoqueRepository : RepositoryBase<Estoque, BarraFisikContext>, IEstoqueRepository
    {
        public IEnumerable<Estoque> GetEstoques()
        {
            return DbSet.Include("Armazem").Include("Produtos").ToList();
        }

        public IEnumerable<Estoque> GetByArmazem(Guid id)
        {
            return DbSet.Include("Produtos").Where(c => c.ArmazemId == id).ToList();
        }

        public override Estoque GetById(Guid id)
        {
            return DbSet.Include("Produtos").Include("Armazem").AsNoTracking().FirstOrDefault(c => c.EstoqueId == id);            
        }
    }
}