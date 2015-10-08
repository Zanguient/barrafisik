using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class LogMensalidadesConfiguration : EntityTypeConfiguration<LogMensalidades>
    {
        public LogMensalidadesConfiguration()
        {
            ToTable("LogMensalidades");

            HasKey(l => l.LogMensalidadesId);

            Property(m => m.AnoReferencia).IsRequired();
            Property(m => m.MesReferencia).IsRequired();
            Property(m => m.ValorPago).IsRequired().HasPrecision(6, 2);
            Property(m => m.DataPagamento).IsRequired();
            Property(l => l.Data).IsRequired();
            Property(l => l.UserId).IsRequired();
            Property(l => l.UsuarioNome).IsRequired();
            Property(l => l.Acao).IsRequired();

        }
    }
}