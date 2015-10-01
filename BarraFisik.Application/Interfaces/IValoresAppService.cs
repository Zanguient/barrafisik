using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IValoresAppService : IDisposable
    {
        void Add(ValoresViewModel valoresViewModel);
        ValoresViewModel GetById(Guid id);
        IEnumerable<ValoresViewModel> GetAll();
        void Update(ValoresViewModel valoresViewModel);
        void Remove(Guid id);
        ValoresViewModel GetValorCliente(int qtdDias, int horario);
    }
}