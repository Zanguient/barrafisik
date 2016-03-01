using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IMovimentacaoEstoqueAppService : IDisposable
    {
        void Add(MovimentacaoEstoqueViewModel movimentacaoViewModel);
        MovimentacaoEstoqueViewModel GetById(Guid id);
        IEnumerable<MovimentacaoEstoqueViewModel> GetAll();
        IEnumerable<MovimentacaoEstoqueViewModel> GetMovimentacoes();
        IEnumerable<MovimentacaoEstoqueViewModel> GetByEstoque(Guid id);
        bool Update(MovimentacaoEstoqueViewModel movimentacaoViewModel);
        bool Remove(Guid id);        
    }
}