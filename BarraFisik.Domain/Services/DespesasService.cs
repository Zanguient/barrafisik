using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class DespesasService : ServiceBase<Despesas>, IDespesasService
    {
        private readonly IDespesasRepository _despesasRepository;

        public DespesasService(IDespesasRepository despesasRepository):base(despesasRepository)
        {
            _despesasRepository = despesasRepository;
        }

        public IEnumerable<Despesas> GetDespesas()
        {
            return _despesasRepository.GetDespesas();
        }
    }
}