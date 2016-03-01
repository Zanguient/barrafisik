using System;

namespace BarraFisik.Domain.ValueObjects
{
    public class SearchVendas
    {
        public DateTime VendaInicio { get; set; }
        public DateTime VendaFim { get; set; }

        public DateTime PagamentoInicio { get; set; }
        public DateTime PagamentoFim { get; set; }

        public DateTime VencimentoInicio { get; set; }
        public DateTime VencimentoFim { get; set; }
    }
}