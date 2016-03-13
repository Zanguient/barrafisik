using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IVendasRepositoryReadOnly
    {
        IEnumerable<Vendas> SearchVendas(SearchVendas sv);
        List<int> GetVendasAnual(int ano);
        void DeleteVenda(Vendas venda);
    }
}