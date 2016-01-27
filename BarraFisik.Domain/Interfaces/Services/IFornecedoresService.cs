using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface IFornecedoresService : IServiceBase<Fornecedores>
    {
        IEnumerable<Fornecedores> GetAllAtivos();
    }
}