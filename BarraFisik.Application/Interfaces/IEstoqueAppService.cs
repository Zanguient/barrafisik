using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IEstoqueAppService : IDisposable
    {
        void Add(EstoqueViewModel estoqueViewModel);
        EstoqueViewModel GetById(Guid id);
        IEnumerable<EstoqueViewModel> GetAll();        
        IEnumerable<EstoqueViewModel> GetEstoques();
        void Update(EstoqueViewModel estoqueViewModel);
        void Remove(Guid id);

        bool ExisteEstoque(Guid armazemId, Guid produtoId);
        IEnumerable<EstoqueViewModel> GetByArmazem(Guid id);
    }
}