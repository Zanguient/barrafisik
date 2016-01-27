using System.Collections.Generic;
using System.Linq;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Repository
{
    public class FornecedoresRepository : RepositoryBase<Fornecedores, BarraFisikContext>, IFornecedoresRepository
    {
        public IEnumerable<Fornecedores> GetAllAtivos()
        {
            return DbSet.Where(c => c.isAtivo).ToList();
        } 
    }
}