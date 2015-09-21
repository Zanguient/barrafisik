using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IFilaEsperaAppService : IDisposable
    {
        void Add(FilaEsperaViewModel filaEsperaViewModel);
        FilaEsperaViewModel GetById(Guid id);
        IEnumerable<FilaEsperaViewModel> GetAll();
        void Update(FilaEsperaViewModel filaEsperaViewModel);
        void Remove(Guid id);
    }
}