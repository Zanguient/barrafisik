using System;
using System.Collections.Generic;
using BarraFisik.Application.Validation;
using BarraFisik.Application.ViewModels;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Application.Interfaces
{
    public interface IMensalidadesAppService : IDisposable
    {
        void Add(MensalidadesViewModel mensalidadesViewModel);
        ValidationAppResult AdicionarMensalidade(MensalidadesViewModel mensalidadesViewModel);
        MensalidadesViewModel GetById(Guid id);
        IEnumerable<MensalidadesViewModel> GetAll();
        IEnumerable<MensalidadesViewModel> GetMensalidadesCliente(Guid id);
        void Update(MensalidadesViewModel mensalidadesViewModel);
        void Remove(Guid id);
    }
}