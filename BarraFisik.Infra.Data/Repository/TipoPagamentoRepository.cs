using System;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Infra.Data.Context;
using System.Linq;

namespace BarraFisik.Infra.Data.Repository
{
    public class TipoPagamentoRepository : RepositoryBase<TipoPagamento, BarraFisikContext>, ITipoPagamentoRepository
    {       
        public TipoPagamento GetByIdInt(int id)
        {
            return DbSet.Where(c => c.TipoPagamentoId == id).FirstOrDefault();
        }

        public void Delete(int id)
        {
            DbSet.Remove(GetByIdInt(id));
        }
    }
}
