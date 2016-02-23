using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IVendasProdutosService : IServiceBase<VendasProdutos>
    {
        void AddVendasProdutos(IEnumerable<VendasProdutos> vendasProdutosList, Guid idVenda);
        IEnumerable<VendasProdutos> GetByVenda(Guid vendaId);
    }
}