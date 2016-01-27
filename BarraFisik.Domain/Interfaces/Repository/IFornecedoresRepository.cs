using System.Collections.Generic;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface IFornecedoresRepository : IRepositoryBase<Fornecedores>
    {
        IEnumerable<Fornecedores> GetAllAtivos();
    }
}
