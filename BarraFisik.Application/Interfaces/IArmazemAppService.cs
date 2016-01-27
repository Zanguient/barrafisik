using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IArmazemAppService : IDisposable
    {
        void Add(ArmazemViewModel armazemViewModel);
        ArmazemViewModel GetById(Guid id);
        IEnumerable<ArmazemViewModel> GetAll();
        void Update(ArmazemViewModel armazemViewModel);
        void Remove(Guid id);
    }
}