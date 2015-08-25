using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository.ReadOnly
{
    public interface IClienteRepositoryReadOnly
    {
        IEnumerable<Cliente> GetAll();
    }
}