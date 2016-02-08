using System;

namespace BarraFisik.Domain.Entities
{
    public class ReceitasAvaliacaoFisica
    {
        public ReceitasAvaliacaoFisica()
        {
            ReceitasAvaliacaoFisicaId = Guid.NewGuid();
            DataEmissao = DateTime.Now;
        }

        public Guid ReceitasAvaliacaoFisicaId { get; set; }
        public decimal Valor { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataEmissao { get; set; }
        public virtual Cliente Cliente { get; set; }

        public Guid CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }

        public Guid SubCategoriaFinanceiraId { get; set; }
        public virtual SubCategoriaFinanceira SubCategoriaFinanceira { get; set; }

        public int TipoPagamentoId { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }
    }
}