using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IVendasProdutosRepository : IRepositoryBase<VendasProdutos>
    {
        IEnumerable<VendasProdutos> GetByVenda(Guid vendaId);
    }
}
