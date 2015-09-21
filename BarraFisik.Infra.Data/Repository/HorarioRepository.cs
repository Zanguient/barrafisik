using System;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class HorarioRepository : RepositoryBase<Horario, BarraFisikContext>, IHorarioRepository
    {
        public Horario GetHorarioCliente(Guid id)
        {
            return base.DbSet.FirstOrDefault(h => h.ClienteId == id);
        }
    }
}