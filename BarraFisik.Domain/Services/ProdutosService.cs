using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class ProdutosService : ServiceBase<Produtos>, IProdutosService
    {
        private readonly IProdutosRepository _produtosRepository;

        public ProdutosService(IProdutosRepository produtosRepository) : base(produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }

        public IEnumerable<Produtos> GetProdutos()
        {
            return _produtosRepository.GetProdutos();
        }

        public IEnumerable<Produtos> GetByCategoria(Guid id)
        {
            return _produtosRepository.GetByCategoria(id);
        }
    }
}
