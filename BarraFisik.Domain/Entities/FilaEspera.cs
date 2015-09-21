using System;

namespace BarraFisik.Domain.Entities
{
    public class FilaEspera
    {
        public FilaEspera()
        {
            FilaEsperaId = Guid.NewGuid();
        }

        public Guid FilaEsperaId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime DataReserva { get; set; }
        public int? Hora { get; set; }
    }
}