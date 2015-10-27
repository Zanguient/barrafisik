using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class ReceitasService : ServiceBase<Receitas>, IReceitasService
    {
        private readonly IReceitasRepository _receitasRepository;
        private readonly IReceitasRepositoryReadOnly _receitasRepositoryReadOnly;

        public ReceitasService(IReceitasRepository receitasRepository, IReceitasRepositoryReadOnly receitasRepositoryReadOnly):base(receitasRepository)
        {
            _receitasRepository = receitasRepository;
            _receitasRepositoryReadOnly = receitasRepositoryReadOnly;
        }

        public IEnumerable<Receitas> GetReceitas()
        {
            return _receitasRepositoryReadOnly.GetReceitas();
        }
    }
}