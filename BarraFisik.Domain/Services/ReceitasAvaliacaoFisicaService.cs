using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class ReceitasAvaliacaoFisicaService : ServiceBase<ReceitasAvaliacaoFisica>, IReceitasAvaliacaoFisicaService
    {
        private readonly IReceitasAvaliacaoFisicaRepository _receitasAvaliacaoFisicaRepository;

        public ReceitasAvaliacaoFisicaService(IReceitasAvaliacaoFisicaRepository receitasAvaliacaoFisicaRepository):base(receitasAvaliacaoFisicaRepository)
        {
            _receitasAvaliacaoFisicaRepository = receitasAvaliacaoFisicaRepository;
        }

        public IEnumerable<ReceitasAvaliacaoFisica> GetByCliente(Guid id)
        {
            return _receitasAvaliacaoFisicaRepository.GetByCliente(id);
        }
    }
}