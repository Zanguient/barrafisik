using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class ValoresConfiguration : EntityTypeConfiguration<Valores>
    {
        public ValoresConfiguration()
        {
            ToTable("Valores");

            HasKey(v => v.ValoresId);

            Property(v => v.QtdDias).IsRequired();
            Property(v => v.Valor).IsRequired().HasPrecision(6,2);
            Property(v => v.HorarioInicio).IsRequired();
            Property(v => v.HorarioFim).IsRequired();
        }
    }
}