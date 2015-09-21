using System;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IHorarioRepository : IRepositoryBase<Horario>
    {
        Horario GetHorarioCliente(Guid id);
    }
}