using System;

namespace BarraFisik.Domain.Entities
{
    public class Armazem
    {
        public Armazem()
        {
            ArmazemId = Guid.NewGuid();
        }

        public Guid ArmazemId { get; set; }
        public string Descricao { get; set; }
    }
}
