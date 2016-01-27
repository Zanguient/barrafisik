using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IFuncionariosRepository : IRepositoryBase<Funcionarios>
    {
        IEnumerable<Funcionarios> GetAllAtivos();
    }
}
