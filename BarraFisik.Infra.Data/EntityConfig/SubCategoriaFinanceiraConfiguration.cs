using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class SubCategoriaFinanceiraConfiguration : EntityTypeConfiguration<SubCategoriaFinanceira>
    {
        public SubCategoriaFinanceiraConfiguration()
        {
            ToTable("SubCategoriaFinanceira");

            HasKey(c => c.SubCategoriaFinanceiraId);

            Property(c => c.SubCategoria).IsRequired().HasMaxLength(100);

            HasRequired(d => d.CategoriaFinanceira).WithMany().HasForeignKey(r => r.CategoriaFinanceiraId);
        }
    }
}