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
            Property(d => d.Documento).IsOptional().HasMaxLength(100);            
            Property(d => d.Valor).HasPrecision(10, 2);
            Property(d => d.Juros).HasPrecision(10, 2);
            Property(d => d.Multa).HasPrecision(10, 2);
            Property(d => d.ValorTotal).HasPrecision(10, 2);
            Property(d => d.Observacao).IsOptional().HasMaxLength(250);
            Property(d => d.Situacao).IsOptional().HasMaxLength(50);
            
            HasRequired(d => d.CategoriaFinanceira).WithMany().HasForeignKey(r => r.CategoriaFinanceiraId);
        }
    }
}