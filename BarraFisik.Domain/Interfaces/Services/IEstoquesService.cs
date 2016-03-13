using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IEstoqueService : IServiceBase<Estoque>
    {
        IEnumerable<Estoque> GetEstoques();
        IEnumerable<Estoque> GetByArmazem(Guid id);
        bool ExisteEstoque(Guid armazemId, Guid produtoId);
        void AtualizaProdutos(Estoque estoque);
    }
}