using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IFuncionariosService : IServiceBase<Funcionarios>
    {
        IEnumerable<Funcionarios> GetAllAtivos();
    }
}