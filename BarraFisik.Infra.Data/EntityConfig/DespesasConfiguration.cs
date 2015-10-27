using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class DespesasConfiguration : EntityTypeConfiguration<Despesas>
    {
        public DespesasConfiguration()
        {
            ToTable("Despesas");

            HasKey(d => d.DespesasId);

            Property(d => d.Observacao).IsOptional().HasMaxLength(250);
            Property(d => d.Valor).HasPrecision(10, 2);
            Property(d => d.Nome).IsRequired();

            HasRequired(d => d.CategoriaFinanceira).WithMany().HasForeignKey(r => r.CategoriaFinanceiraId);

            Ignore(d => d.DataDespesa);
        }
    }
}