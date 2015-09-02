using BarraFisik.Domain.Entities;

namespace BarraFisik.Domain.ValueObjects
{
    public class ClienteHorario
    {
        public Cliente Cliente { get; set; }
        public Horario Horario { get; set; }
    }
}