using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IVendasService : IServiceBase<Vendas>
    {
        IEnumerable<Vendas> GetVendas();
        IEnumerable<Vendas> GetPendentes(int mes, int ano);
        IEnumerable<Vendas> SearchVendas(SearchVendas sv);
        List<int> GetVendasAnual(int ano);
        IEnumerable<Vendas> GetVendasByCliente(Guid idCliente);
        void DeleteVenda(Vendas venda);
    }
}