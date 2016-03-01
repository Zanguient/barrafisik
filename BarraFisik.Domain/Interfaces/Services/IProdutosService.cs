using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IProdutosService : IServiceBase<Produtos>
    {
        IEnumerable<Produtos> GetProdutos();
        IEnumerable<Produtos> GetByCategoria(Guid id);
    }
}