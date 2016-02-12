using System;

namespace BarraFisik.Domain.ValueObjects
{
    public class RelatorioFinanceiro
    {
        public string Documento { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string Observacao { get; set; }
        public decimal Valor { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal ValorTotal { get; set; }
        public string Situacao { get; set; }

        public string Categoria { get; set; }
        public string Tipo { get; set; }
        public string SubCategoria { get; set; }

        public string Cliente { get; set; }
        public string Nome { get; set; }
        public string Funcionario { get; set; }
        public string Fornecedor { get; set; }
        public string TipoPagamento { get; set; }
        public bool isPersonal { get; set; }
        public decimal ValorPersonal { get; set; }

        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }
    }
}