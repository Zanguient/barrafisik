using System;
using System.Collections.Generic;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.Application.Interfaces
{
    public interface IHorarioAppService : IDisposable
    {
        void Add(HorarioViewModel horarioViewModel);
        HorarioViewModel GetById(Guid id);
        IEnumerable<HorarioViewModel> GetAll();
        void Update(ClienteHorarioViewModel clienteHorarioViewModel);
        void Remove(HorarioViewModel horarioViewModel);

        HorarioViewModel GetHorarioCliente(Guid id);

        TotalHorarioViewModel GetHorarios();
    }
}