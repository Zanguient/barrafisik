using System;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class RelatorioFinanceiroViewModel
    {
        public Guid RelatorioFinanceiroId { get; set; }
        public DateTime Data { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Observacao { get; set; }

        public string Categoria { get; set; }
        public string Tipo { get; set; }

        public Guid RegistroId { get; set; }
    }
}