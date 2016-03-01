using System;

namespace BarraFisik.Domain.Entities
{
    public class Estoque
    {
        public Estoque()
        {
            EstoqueId = Guid.NewGuid();
        }

        public Guid EstoqueId { get; set; }
        public Guid ArmazemId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }       
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal SaldoVenda { get; set; }
        public int TotalVendido { get; set; }

        public virtual Produtos Produtos { get; set; }
        public virtual Armazem Armazem { get; set; }
    }
}