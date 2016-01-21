using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Repository
{
    public interface ITipoPagamentoRepository : IRepositoryBase<TipoPagamento>
    {
        TipoPagamento GetByIdInt(int id);
        void Delete(int id);
    }
}
