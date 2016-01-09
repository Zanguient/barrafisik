using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class ReceitasAvaliacaoFisicaConfiguration : EntityTypeConfiguration<ReceitasAvaliacaoFisica>
    {
        public ReceitasAvaliacaoFisicaConfiguration()
        {
            ToTable("ReceitasAvaliacoesFisicas");

            HasKey(r => r.ReceitasAvaliacaoFisicaId);

            Property(r => r.Valor).HasPrecision(5, 2);

            HasRequired(r => r.Cliente).WithMany().HasForeignKey(r => r.ClienteId);
            HasRequired(m => m.CategoriaFinanceira).WithMany().HasForeignKey(m => m.CategoriaFinanceiraId);
        }
    }
}