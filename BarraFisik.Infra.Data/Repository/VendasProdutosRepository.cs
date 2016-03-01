using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class VendasProdutosRepository : RepositoryBase<VendasProdutos, BarraFisikContext>, IVendasProdutosRepository
    {
        public IEnumerable<VendasProdutos> GetByVenda(Guid vendaId)
        {
            return DbSet.Include("Estoque.Produtos").Include("Vendas").Where(c => c.VendaId == vendaId).ToList();
        }
    }
}