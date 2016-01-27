using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IFornecedoresAppService : IDisposable
    {
        void Add(FornecedoresViewModel fornecedoresViewModel);
        FornecedoresViewModel GetById(Guid id);
        IEnumerable<FornecedoresViewModel> GetAll();
        IEnumerable<FornecedoresViewModel> GetAllAtivos();
        void Update(FornecedoresViewModel fornecedoresViewModel);
        void Remove(Guid id);
    }
}