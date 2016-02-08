using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class ReceitasAvaliacaoFisicaViewModel
    {
        public ReceitasAvaliacaoFisicaViewModel()
        {
            ReceitasAvaliacaoFisicaId = Guid.NewGuid();
            TipoPagamentoId = 0;
            DataEmissao = DateTime.Now;
        }

        [Key]
        public Guid ReceitasAvaliacaoFisicaId { get; set; }

        [Required(ErrorMessage = "Informe o valor do pagamento")]
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 999.99)]
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