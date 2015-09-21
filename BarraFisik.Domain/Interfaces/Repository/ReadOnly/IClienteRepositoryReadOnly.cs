using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IClienteRepositoryReadOnly
    {
        IEnumerable<Cliente> GetAll();
        ClienteHorario GetByClienteId(Guid id);
        void UpdateClientesPendentes(int mes, int ano);
        IEnumerable<Cliente> GetClientesSituacao(string situacao);
    }
}