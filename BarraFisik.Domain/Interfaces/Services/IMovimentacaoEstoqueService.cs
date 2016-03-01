using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IMovimentacaoEstoqueService : IServiceBase<MovimentacaoEstoque>
    {
        IEnumerable<MovimentacaoEstoque> GetMovimentacoes();
        IEnumerable<MovimentacaoEstoque> GetByEstoque(Guid id);
    }
}