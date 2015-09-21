using System;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Services
{
    public class HorarioService : ServiceBase<Horario>, IHorarioService
    {
        private readonly IHorarioRepository _horarioRepository;
        private readonly IHorarioRepositoryReadOnly _horarioRepositoryReadOnly;

        public HorarioService(IHorarioRepository horarioRepository, IHorarioRepositoryReadOnly horarioRepositoryReadOnly) : base(horarioRepository)
        {
            _horarioRepository = horarioRepository;
            _horarioRepositoryReadOnly = horarioRepositoryReadOnly;
        }

        public TotalHorario GetHorarios()
        {
            return _horarioRepositoryReadOnly.ListaHorarios();
        }

        public Horario GetHorarioCliente(Guid id)
        {
            return _horarioRepository.GetHorarioCliente(id);
        }
    }
}