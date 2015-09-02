using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IClienteService : IServiceBase<Cliente>
    {
        ValidationResult AdicionarCliente(Cliente cliente);
        ValidationResult AtualizarCliente(Cliente cliente);
        IEnumerable<Cliente> GetClientes();
        Cliente GetClienteHorario(Guid id);
    }
}