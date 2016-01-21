using System;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class TipoPagamentoService : ServiceBase<TipoPagamento>, ITipoPagamentoService
    {
        private readonly ITipoPagamentoRepository _tipoPagamentoRepository;

        public TipoPagamentoService(ITipoPagamentoRepository tipoPagamentoRepository) : base(tipoPagamentoRepository)
        {
            _tipoPagamentoRepository = tipoPagamentoRepository;
        }

        public TipoPagamento GetByIdInt(int id)
        {
           return _tipoPagamentoRepository.GetByIdInt(id);
        }

        public void Delete(int id)
        {
            _tipoPagamentoRepository.Delete(id);
        }
    }
}
