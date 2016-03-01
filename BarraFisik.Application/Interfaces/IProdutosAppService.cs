using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IProdutosAppService : IDisposable
    {
        void Add(ProdutosViewModel produtosViewModel);
        ProdutosViewModel GetById(Guid id);
        IEnumerable<ProdutosViewModel> GetAll();
        IEnumerable<ProdutosViewModel> GetProdutos();
        IEnumerable<ProdutosViewModel> GetByCategoria(Guid id);
        void Update(ProdutosViewModel produtosViewModel);
        void Remove(Guid id);
    }
}