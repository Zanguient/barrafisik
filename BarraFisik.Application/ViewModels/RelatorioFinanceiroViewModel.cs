using System;

namespace BarraFisik.Application.ViewModels
{
    public class RelatorioFinanceiroViewModel
    {
        public DateTime Data { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Observacao { get; set; }

        public string Categoria { get; set; }
        public string Tipo { get; set; }

        public Guid RegistroId { get; set; }
    }
}