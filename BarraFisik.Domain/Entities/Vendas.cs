using System;

namespace BarraFisik.Domain.Entities
{
    public class Vendas
    {
        public Vendas()
        {
            VendaId = Guid.NewGuid();
        }

        public Guid VendaId { get; set; }
        public Guid? ClienteId { get; set; }
        public int? TipoPagamentoId { get; set; }
        public Guid EstoqueId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public Guid ReceitasId { get; set; }
        public string Nome { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }
        public virtual Estoque Estoque { get; set; }
        public virtual Receitas Receitas { get; set; }
    }
}