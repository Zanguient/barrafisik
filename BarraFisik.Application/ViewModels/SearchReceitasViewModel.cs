using System;

namespace BarraFisik.Application.ViewModels
{
    public class SearchReceitasViewModel
    {
        public DateTime VencimentoInicio { get; set; }
        public DateTime VencimentoFim { get; set; }

        public DateTime PagamentoInicio { get; set; }
        public DateTime PagamentoFim { get; set; }

        public DateTime EmissaoInicio { get; set; }
        public DateTime EmissaoFim { get; set; }

    }
}