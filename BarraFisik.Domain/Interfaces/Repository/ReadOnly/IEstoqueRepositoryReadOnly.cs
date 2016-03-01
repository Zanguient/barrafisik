using System;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IEstoqueRepositoryReadOnly
    {
        bool ExisteEstoque(Guid armazemId, Guid produtoId);
    }
}