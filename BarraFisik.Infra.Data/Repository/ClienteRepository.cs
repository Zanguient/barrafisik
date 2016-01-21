using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class ClienteRepository : RepositoryBase<Cliente, BarraFisikContext>, IClienteRepository
    {
        public Cliente GetClienteHorario(Guid id)
        {
            return DbSet.Where(c => c.ClienteId == id).Include("Horario").FirstOrDefault();
        }

        public IEnumerable<Cliente> GetAniversariantes(int mes)
        {
            return
                DbSet.Where(c => c.DtNascimento.Month == mes)
                    .Where(c => c.IsAtivo)
                    .OrderBy(c => c.DtNascimento.Day)
                    .ThenBy(c => c.Nome)
                    .ToList();
        }

        public IEnumerable<Cliente> GetClientes()
        {
            return DbSet.Where(c => c.IsAtivo).ToList();
        }
    }
}