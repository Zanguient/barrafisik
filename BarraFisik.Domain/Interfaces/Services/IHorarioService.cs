using System;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IHorarioService : IServiceBase<Horario>
    {
        TotalHorario GetHorarios();
        Horario GetHorarioCliente(Guid id);
    }
}