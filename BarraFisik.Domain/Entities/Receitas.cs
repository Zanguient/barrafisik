using System;

namespace BarraFisik.Domain.Entities
{
    public class Receitas
    {
        public Receitas()
        {
            ReceitasId = Guid.NewGuid();
        }

        public Guid ReceitasId { get; set; }
        public string Documento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal Valor { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
        public string Situacao { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }

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