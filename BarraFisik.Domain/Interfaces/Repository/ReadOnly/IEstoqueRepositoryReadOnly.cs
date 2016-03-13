using System;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IEstoqueRepositoryReadOnly
    {
        bool ExisteEstoque(Guid armazemId, Guid produtoId);
        void AtualizaProdutos(Estoque estoque);
    }
}