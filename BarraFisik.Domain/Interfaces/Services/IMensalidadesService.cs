using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.ValueObjects;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IMensalidadesService : IServiceBase<Mensalidades>
    {
        ValidationResult AdicionarMensalidade(Mensalidades mensalidade);
        IEnumerable<Mensalidades> GetMensalidadesCliente(Guid id);
    }
}