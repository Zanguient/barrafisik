using System;

namespace BarraFisik.Domain.Entities
{
    public class ReceitasAvaliacaoFisica
    {
        public ReceitasAvaliacaoFisica()
        {
            ReceitasAvaliacaoFisicaId = Guid.NewGuid();
        }

        public Guid ReceitasAvaliacaoFisicaId { get; set; }
        public decimal Valor { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime DataPagamento { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}