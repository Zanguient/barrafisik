using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IMensalidadesRepositoryReadOnly
    {
        IEnumerable<Mensalidades> GetMensalidadesCliente(Guid id);
        bool ExisteMensalidade(Mensalidades mensalidade);
    }
}