using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Cliente GetClienteHorario(Guid id);
        IEnumerable<Cliente> GetAniversariantes(int mes);
        IEnumerable<Cliente> GetClientes();
        Cliente GetByIdMensalidade(Guid? id);
    }
}