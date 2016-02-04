using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class DespesasViewModel
    {
        public DespesasViewModel()
        {
            DespesasId = Guid.NewGuid();
        }

        [Key]
        public Guid DespesasId { get; set; }
        public string Documento { get; set; }
        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "Informe a Data de Vencimento")]
        public DateTime DataVencimento { get; set; }
        public DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "Informe a Valor")]
        public decimal Valor { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
        public string Situacao { get; set; }

        [Required(ErrorMessage = "Informe a Categoria Financeira")]
        public Guid CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }

        public Guid? SubCategoriaFinanceiraId { get; set; }
        public virtual SubCategoriaFinanceira SubCategoriaFinanceira { get; set; }

        public int? TipoPagamentoId { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }

        public Guid? FornecedorId { get; set; }
        public virtual Fornecedores Fornecedores { get; set; }

        public Guid? FuncionarioId { get; set; }
        public virtual Funcionarios Funcionarios { get; set; }
    }
}