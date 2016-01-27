using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IFuncionariosAppService : IDisposable
    {
        void Add(FuncionariosViewModel funcionariosViewModel);
        FuncionariosViewModel GetById(Guid id);
        IEnumerable<FuncionariosViewModel> GetAll();
        IEnumerable<FuncionariosViewModel> GetAllAtivos();
        void Update(FuncionariosViewModel funcionariosViewModel);
        void Remove(Guid id);
    }
}