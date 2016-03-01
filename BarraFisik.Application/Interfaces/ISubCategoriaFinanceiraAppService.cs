using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface ISubCategoriaFinanceiraAppService : IDisposable
    {
        void Add(SubCategoriaFinanceiraViewModel subCategoriaFinanceiraViewModel);
        SubCategoriaFinanceiraViewModel GetById(Guid id);
        IEnumerable<SubCategoriaFinanceiraViewModel> GetAll();
        IEnumerable<SubCategoriaFinanceiraViewModel> GetByCategoria(Guid idCategoria);
        void Update(SubCategoriaFinanceiraViewModel subCategoriaFinanceiraViewModel);
        void Remove(Guid id);
    }
}