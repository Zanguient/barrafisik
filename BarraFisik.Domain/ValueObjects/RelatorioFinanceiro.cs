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
    }
}