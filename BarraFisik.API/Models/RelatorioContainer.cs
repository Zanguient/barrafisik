using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarraFisik.Application.ViewModels;

namespace BarraFisik.API.Models
{
    public class RelatorioContainer
    {
        public IEnumerable<RelatorioFinanceiroViewModel> ListRelatorio { get; set; }
        public IDictionary<string, decimal> TotalByTipoPagamento { get; set; }
    }
}