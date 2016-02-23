using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IVendasProdutosAppService : IDisposable
    {
        //void Add(VendasProdutosViewModel vendasProdutosViewModel);
        VendasProdutosViewModel GetById(Guid id);
        IEnumerable<VendasProdutosViewModel> GetAll();
        IEnumerable<VendasProdutosViewModel> GetByVenda(Guid vendaId);
        //void Update(VendasProdutosViewModel vendasProdutosViewModel);        
        void Remove(Guid id);
        void AddVendasProdutos(IEnumerable<VendasProdutosViewModel> vendasProdutosList, Guid idVenda);
        void AddProduto(VendasProdutosViewModel vendasProdutosViewModel);
    }
}