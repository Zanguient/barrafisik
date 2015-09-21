using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class HorarioConfiguration : EntityTypeConfiguration<Horario>
    {
        public HorarioConfiguration()
        {
            ToTable("Horario");

            HasKey(h => h.HorarioId); 

            Property(h => h.HSegunda).IsOptional().HasMaxLength(20);
            Property(h => h.HTerca).IsOptional().HasMaxLength(20);
            Property(h => h.HQuarta).IsOptional().HasMaxLength(20);
            Property(h => h.HQuinta).IsOptional().HasMaxLength(20);
            Property(h => h.HSexta).IsOptional().HasMaxLength(20);

            HasRequired(h => h.Cliente)
                .WithMany().HasForeignKey(h => h.ClienteId);

        }
    }
}