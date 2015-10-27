using System;

namespace BarraFisik.Application.ViewModels
{
    public class RelatorioFinanceiroSearchViewModel
    {
        public string Tipo { get; set; }
        public string Categoria { get; set; }
        public string Nome { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}