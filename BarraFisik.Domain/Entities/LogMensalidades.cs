using System;

namespace BarraFisik.Domain.Entities
{
    public class LogMensalidades
    {
        public LogMensalidades()
        {
            LogMensalidadesId = Guid.NewGuid();
        }

        public Guid LogMensalidadesId { get; set; }
        public Guid MensalidadesId { get; set; }
        public decimal ValorPago { get; set; }
        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime DataPagamento { get; set; }
        public string Acao { get; set; }
        public DateTime Data { get; set; }
        public string UserId { get; set; }
        public string UsuarioNome { get; set; }
        public bool isPersonal { get; set; }
        public decimal ValorPersonal { get; set; }
    }
}