using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IProdutosRepository : IRepositoryBase<Produtos>
    {
        IEnumerable<Produtos> GetProdutos();
        IEnumerable<Produtos> GetByCategoria(Guid id);
    }
}