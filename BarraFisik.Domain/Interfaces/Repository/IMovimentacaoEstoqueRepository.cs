using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IMovimentacaoEstoqueRepository : IRepositoryBase<MovimentacaoEstoque>
    {
        IEnumerable<MovimentacaoEstoque> GetMovimentacoes();
        IEnumerable<MovimentacaoEstoque> GetByEstoque(Guid id);
    }
}