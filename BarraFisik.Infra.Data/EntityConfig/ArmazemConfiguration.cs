using BarraFisik.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class ArmazemConfiguration : EntityTypeConfiguration<Armazem>
    {
        public ArmazemConfiguration()
        {
            ToTable("Armazem");

            HasKey(a => a.ArmazemId);

            Property(a => a.Descricao).IsRequired().HasMaxLength(150);
        }
    }
}
