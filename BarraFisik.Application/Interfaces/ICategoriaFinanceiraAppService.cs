using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface ICategoriaFinanceiraAppService : IDisposable
    {
        void Add(CategoriaFinanceiraViewModel categoriaFinanceiraViewModel);
        CategoriaFinanceiraViewModel GetById(Guid id);
        IEnumerable<CategoriaFinanceiraViewModel> GetAll();
        IEnumerable<CategoriaFinanceiraViewModel> GetByTipo(string tipo);
        void Update(CategoriaFinanceiraViewModel categoriaFinanceiraViewModel);
        void Remove(Guid id);
    }
}