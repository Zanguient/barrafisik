using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class VendasViewModel
    {
        public VendasViewModel()
        {
            VendaId = Guid.NewGuid();
        }

        [Key]
        public Guid VendaId { get; set; }

        public Guid? ClienteId { get; set; }
        public int? TipoPagamentoId { get; set; }

        [Required(ErrorMessage = "Informe o estoque")]
        public Guid EstoqueId { get; set; }

        [Required(ErrorMessage = "Informe a Quantidade")]
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
