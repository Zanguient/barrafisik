using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class MensalidadesConfiguration : EntityTypeConfiguration<Mensalidades>
    {
        public MensalidadesConfiguration()
        {
            ToTable("Mensalidades");

            HasKey(m => m.MensalidadesId);

            Property(m => m.AnoReferencia).IsRequired();
            Property(m => m.MesReferencia).IsRequired();
            Property(m => m.ValorPago).IsRequired().HasPrecision(7,2);
            Property(m => m.ValorPersonal).IsOptional().HasPrecision(7,2);
            Property(m => m.DataPagamento).IsRequired();
            Property(m => m.Nome).IsRequired();

            Ignore(m => m.ResultadoValidacao);

            HasRequired(m => m.Cliente)
                .WithMany(m => m.Mensalidades)
                .HasForeignKey(m => m.ClienteId);

            HasRequired(m => m.CategoriaFinanceira).WithMany().HasForeignKey(m => m.CategoriaFinanceiraId);
        }
    }
}