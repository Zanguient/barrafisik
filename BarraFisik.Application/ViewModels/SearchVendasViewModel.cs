﻿using System;

namespace BarraFisik.Application.ViewModels
{
    public class SearchVendasViewModel
    {
        public DateTime VendaInicio { get; set; }
        public DateTime VendaFim { get; set; }

        public DateTime PagamentoInicio { get; set; }
        public DateTime PagamentoFim { get; set; }

        public DateTime VencimentoInicio { get; set; }
        public DateTime VencimentoFim { get; set; }

    }
}