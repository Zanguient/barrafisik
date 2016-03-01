using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class ProdutosRepository : RepositoryBase<Produtos, BarraFisikContext>, IProdutosRepository
    {
        public IEnumerable<Produtos> GetProdutos()
        {
            return DbSet.Include("ProdutosCategoria").ToList();
        }

        public IEnumerable<Produtos> GetByCategoria(Guid id)
        {
            return DbSet.ToList().Where(c => c.ProdutoCategoriaId == id);
        }
    }
}