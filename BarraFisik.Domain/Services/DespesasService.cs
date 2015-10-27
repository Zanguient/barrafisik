using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class DespesasService : ServiceBase<Despesas>, IDespesasService
    {
        private readonly IDespesasRepository _despesasRepository;
        private readonly IDespesasRepositoryReadOnly _despesasRepositoryReadOnly;

        public DespesasService(IDespesasRepository despesasRepository, IDespesasRepositoryReadOnly despesasRepositoryReadOnly):base(despesasRepository)
        {
            _despesasRepository = despesasRepository;
            _despesasRepositoryReadOnly = despesasRepositoryReadOnly;
        }

        public IEnumerable<Despesas> GetDespesas()
        {
            return _despesasRepositoryReadOnly.GetDespesas();
        }
    }
}