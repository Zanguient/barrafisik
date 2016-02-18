using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class MovimentacaoEstoqueService : ServiceBase<MovimentacaoEstoque>, IMovimentacaoEstoqueService
    {
        private readonly IMovimentacaoEstoqueRepository _estoqueRepository;

        public MovimentacaoEstoqueService(IMovimentacaoEstoqueRepository estoqueRepository) : base(estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        public IEnumerable<MovimentacaoEstoque> GetMovimentacoes()
        {
            return _estoqueRepository.GetMovimentacoes();
        }

        public IEnumerable<MovimentacaoEstoque> GetByEstoque(Guid id)
        {
            return _estoqueRepository.GetByEstoque(id);
        }
    }
}
