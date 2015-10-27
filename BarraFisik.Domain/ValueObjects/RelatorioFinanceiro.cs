using System;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.ValueObjects
{
    public class RelatorioFinanceiro
    {
        public RelatorioFinanceiro()
        {
            RelatorioFinanceiroId = Guid.NewGuid();
        }

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