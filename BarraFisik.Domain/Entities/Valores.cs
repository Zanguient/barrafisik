using System;

namespace BarraFisik.Domain.Entities
{
    public class Valores
    {
        public Valores()
        {
            ValoresId = Guid.NewGuid();
        }

        public Guid ValoresId { get; set; }
        public int QtdDias { get; set; }
        public decimal Valor { get; set; }
        public int HorarioInicio { get; set; }
        public int  HorarioFim { get; set; }
    }
}