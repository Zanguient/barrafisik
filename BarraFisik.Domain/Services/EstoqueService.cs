using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;

namespace BarraFisik.Domain.Services
{
    public class EstoqueService : ServiceBase<Estoque>, IEstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IEstoqueRepositoryReadOnly _estoqueRepositoryReadOnly;

        public EstoqueService(IEstoqueRepository estoqueRepository, IEstoqueRepositoryReadOnly estoqueRepositoryReadOnly) : base(estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
            _estoqueRepositoryReadOnly = estoqueRepositoryReadOnly;
        }

        public IEnumerable<Estoque> GetEstoques()
        {
            return _estoqueRepository.GetEstoques();
        }

        public IEnumerable<Estoque> GetByArmazem(Guid id)
        {
            return _estoqueRepository.GetByArmazem(id);
        }

        public bool ExisteEstoque(Guid armazemId, Guid produtoId)
        {
            return _estoqueRepositoryReadOnly.ExisteEstoque(armazemId, produtoId);
        }

        public void AtualizaProdutos(Estoque estoque)
        {
            _estoqueRepositoryReadOnly.AtualizaProdutos(estoque);
        }
    }
}
