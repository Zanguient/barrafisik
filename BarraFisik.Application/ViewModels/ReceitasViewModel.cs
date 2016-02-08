using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class ReceitasViewModel
    {
        public ReceitasViewModel()
        {
            ReceitasId = Guid.NewGuid();
        }

        [Key]
        public Guid ReceitasId { get; set; }

        public string Documento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "Informe o Valor")]
        public decimal Valor { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal ValorTotal { get; set; }

        [MaxLength(250, ErrorMessage = "Máximo {0} caracteres")]
        public string Observacao { get; set; }
        public string Situacao { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }


        //Referente à mensalidade
        public int? MesReferencia { get; set; }
        public int? AnoReferencia { get; set; }
        public bool isPersonal { get; set; }
        public decimal ValorPersonal { get; set; }


        //[Required(ErrorMessage = "Informe a Categoria Financeira")]
        public Guid CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }

        public Guid? SubCategoriaFinanceiraId { get; set; }
        public virtual SubCategoriaFinanceira SubCategoriaFinanceira { get; set; }

        public int? TipoPagamentoId { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }

        public Guid? FuncionarioId { get; set; }
        public virtual Funcionarios Funcionarios { get; set; }

        public Guid? ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

    }
}