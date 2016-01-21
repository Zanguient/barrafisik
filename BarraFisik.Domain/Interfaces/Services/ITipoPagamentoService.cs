using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.Interfaces.Services
{
    public interface ITipoPagamentoService : IServiceBase<TipoPagamento>
    {
        TipoPagamento GetByIdInt(int id);
        void Delete(int id);
    }
}
