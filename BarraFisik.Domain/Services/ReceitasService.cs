using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class ReceitasService : ServiceBase<Receitas>, IReceitasService
    {
        private readonly IReceitasRepository _receitasRepository;

        public ReceitasService(IReceitasRepository receitasRepository):base(receitasRepository)
        {
            _receitasRepository = receitasRepository;
        }

        public IEnumerable<Receitas> GetReceitas()
        {
            return _receitasRepository.GetReceitas();
        }
    }
}