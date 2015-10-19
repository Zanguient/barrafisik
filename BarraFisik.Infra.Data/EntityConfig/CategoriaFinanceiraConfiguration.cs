using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class CategoriaFinanceiraConfiguration : EntityTypeConfiguration<CategoriaFinanceira>
    {
        public CategoriaFinanceiraConfiguration()
        {
            ToTable("CategoriaFinanceira");

            HasKey(c => c.CategoriaFinanceiraId);

            Property(c => c.Tipo).IsRequired().HasMaxLength(50);
            Property(c => c.Categoria).IsRequired();
        }
    }
}