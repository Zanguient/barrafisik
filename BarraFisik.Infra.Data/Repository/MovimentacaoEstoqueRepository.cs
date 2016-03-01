using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class MovimentacaoEstoqueRepository : RepositoryBase<MovimentacaoEstoque, BarraFisikContext>,
        IMovimentacaoEstoqueRepository
    {
        public IEnumerable<MovimentacaoEstoque> GetMovimentacoes()
        {
            return DbSet.Include("Fornecedores").Include("Produtos").Include("Armazem").Include("Estoque").ToList();
        }

        public IEnumerable<MovimentacaoEstoque> GetByEstoque(Guid id)
        {
            return DbSet.Include("Fornecedores").Where(c => c.EstoqueId == id).ToList();
        } 
    }
}