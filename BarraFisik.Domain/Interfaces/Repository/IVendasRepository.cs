using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IVendasRepository : IRepositoryBase<Vendas>
    {
        IEnumerable<Vendas> GetVendas();
        IEnumerable<Vendas> GetPendentes(int mes, int ano);
        IEnumerable<Vendas> GetVendasByCliente(Guid idCliente);
    }
}
