using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IProdutosCategoriaAppService : IDisposable
    {
        void Add(ProdutosCategoriaViewModel produtosCategoriaViewModel);
        ProdutosCategoriaViewModel GetById(Guid id);
        IEnumerable<ProdutosCategoriaViewModel> GetAll();
        void Update(ProdutosCategoriaViewModel produtosViewModel);
        void Remove(Guid id);
    }
}