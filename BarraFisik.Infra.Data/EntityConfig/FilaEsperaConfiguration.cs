using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class FilaEsperaConfiguration : EntityTypeConfiguration<FilaEspera>
    {
        public FilaEsperaConfiguration()
        {
            ToTable("FilaEspera");

            HasKey(f => f.FilaEsperaId);

            Property(f => f.Nome).IsRequired();
            Property(f => f.Telefone).IsOptional().HasMaxLength(15);
            Property(f => f.Celular).IsOptional().HasMaxLength(15);
            Property(f => f.Email).IsOptional();
            Property(f => f.DataReserva).IsOptional();
            Property(f => f.Hora).IsOptional();
        }
    }
}