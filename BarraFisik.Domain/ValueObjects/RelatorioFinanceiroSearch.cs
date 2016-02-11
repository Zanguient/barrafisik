using System;

namespace BarraFisik.Domain.ValueObjects
{
    public class RelatorioFinanceiroSearch
    {
        public DateTime EmissaoInicio { get; set; }
        public DateTime EmissaoFim { get; set; }

        public DateTime VencimentoInicio { get; set; }
        public DateTime VencimentoFim { get; set; }

        public DateTime PagamentoInicio { get; set; }
        public DateTime PagamentoFim { get; set; }

        public string Tipo { get; set; }
        public string CategoriaId { get; set; }
        public string SubCategoriaId { get; set; }
        public string Situacao { get; set; }
    }
}