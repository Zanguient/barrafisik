using System;

namespace BarraFisik.Domain.ValueObjects
{
    public class RelatorioFinanceiro
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