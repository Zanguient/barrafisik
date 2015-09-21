using System;

namespace BarraFisik.Domain.Entities
{
    public class Horario
    {
        public Horario()
        {
            HorarioId = Guid.NewGuid();
        }

        public Guid HorarioId { get; set; }

        public bool Segunda { get; set; }
        public string HSegunda { get; set; }

        public bool Terca { get; set; }
        public string HTerca { get; set; }

        public bool Quarta { get; set; }
        public string HQuarta { get; set; }

        public bool Quinta { get; set; }
        public string HQuinta { get; set; }

        public bool Sexta { get; set; }
        public string HSexta { get; set; }

        public Cliente Cliente { get; set; }
        public Guid ClienteId { get; set; }
    }
}