using System;

namespace BarraFisik.Domain.Entities
{
    public class VendasProdutos
    {
        public VendasProdutos()
        {
            VendasProdutosId = Guid.NewGuid();
        }

        public Guid VendasProdutosId { get; set; }
        public Guid VendaId { get; set; }
        public Guid EstoqueId { get; set; }
        public int Quantidade { get; set; }

        public Estoque Estoque { get; set; }
        public Vendas Vendas { get; set; }        
    }
}