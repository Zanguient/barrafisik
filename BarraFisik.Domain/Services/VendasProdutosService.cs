using System;
using System.Collections.Generic;
using BarraFisik.Domain.Entities;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Services;

namespace BarraFisik.Domain.Services
{
    public class VendasProdutosService : ServiceBase<VendasProdutos>, IVendasProdutosService
    {
        private readonly IVendasProdutosRepository _vendasProdutosRepository;

        public VendasProdutosService(IVendasProdutosRepository vendasProdutosRepository) : base(vendasProdutosRepository)
        {
            _vendasProdutosRepository = vendasProdutosRepository;
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
    }
}
