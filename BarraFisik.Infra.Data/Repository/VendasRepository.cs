using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class VendasRepository : RepositoryBase<Vendas, BarraFisikContext>, IVendasRepository
    {
        public IEnumerable<Vendas> GetVendas()
        {
            var today = DateTime.Now;
            return 
                DbSet.Include("Cliente")
                    .Include("TipoPagamento")
                    .Include("Receitas")
                    .Include("Funcionarios")
                    .Where(c => c.DataVenda.Month == today.Month)
                    .Where(c => c.DataVenda.Year == today.Year)
                    .ToList();           
        }

        public IEnumerable<Vendas> GetPendentes(int mes, int ano)
        {
            return
                DbSet.Include("Cliente")
                    .Include("Funcionarios")
                    .Include("Receitas")
                    .Where(c => c.Receitas.Situacao == "Pendente")
                    .Where(c => c.DataVencimento.Month == mes)
                    .Where(c => c.DataVencimento.Year == ano)
                    .ToList();
        }

        public IEnumerable<Vendas> GetVendasByCliente(Guid idCliente)
        {
            return DbSet.Include("Receitas")
                .Include("TipoPagamento")
                .Where(c => c.ClienteId == idCliente)
                .ToList();
        }
    }
}