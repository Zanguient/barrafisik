using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class VendasProdutosService : ServiceBase<VendasProdutos>, IVendasProdutosService
    {
        private readonly IVendasProdutosRepository _vendasProdutosRepository;
        private readonly IVendasProdutosRepositoryReadOnly _vendasProdutosRepositoryReadOnly;

        public VendasProdutosService(IVendasProdutosRepository vendasProdutosRepository, IVendasProdutosRepositoryReadOnly vendasProdutosRepositoryReadOnly) : base(vendasProdutosRepository)
        {
            _vendasProdutosRepository = vendasProdutosRepository;
            _vendasProdutosRepositoryReadOnly = vendasProdutosRepositoryReadOnly;
        }

        public void AddVendasProdutos(IEnumerable<VendasProdutos> vendasProdutosList, Guid idVenda)
        {
            foreach (var vp in vendasProdutosList)
            {
                vp.VendaId = idVenda;
                base.Add(vp);
            }
        }

        public IEnumerable<VendasProdutos> GetByVenda(Guid vendaId)
        {
            return _vendasProdutosRepository.GetByVenda(vendaId);
        }

        public void DeleteVendaProduto(VendasProdutos produto)
        {
            _vendasProdutosRepositoryReadOnly.DeleteVendaProduto(produto);
        }
    }
}
