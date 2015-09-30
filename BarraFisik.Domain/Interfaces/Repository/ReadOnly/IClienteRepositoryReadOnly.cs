using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IClienteRepositoryReadOnly
    {
        IEnumerable<ClienteHorario> GetAll();
        IEnumerable<ClienteHorario> GetClientesAll();
        ClienteHorario GetByClienteId(Guid id);
        void UpdateClientesPendentes(int mes, int ano);
        IEnumerable<Cliente> GetClientesSituacao(string situacao);
        TotalInscritos GetTotalInscritos(int ano);
    }
}