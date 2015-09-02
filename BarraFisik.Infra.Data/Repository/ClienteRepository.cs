using System;
using System.Data.Entity;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.ValueObjects;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class ClienteRepository : RepositoryBase<Cliente, BarraFisikContext>, IClienteRepository
    {
        public Cliente GetClienteHorario(Guid id)
        {
            return base.DbSet.Where(c => c.ClienteId == id).Include("Horario").FirstOrDefault();
        }
    }
}