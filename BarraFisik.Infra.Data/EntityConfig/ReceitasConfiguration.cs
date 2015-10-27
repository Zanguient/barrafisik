using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class ReceitasConfiguration : EntityTypeConfiguration<Receitas>
    {
        public ReceitasConfiguration()
        {
            ToTable("Receitas");

            HasKey(r => r.ReceitasId);

            Property(r => r.Observacao).IsOptional().HasMaxLength(250);
            Property(r => r.Valor).HasPrecision(10, 2);
            Property(r => r.Nome).IsRequired();

            HasRequired(r => r.CategoriaFinanceira).WithMany().HasForeignKey(r => r.CategoriaFinanceiraId);

            Ignore(r => r.DataReceita);
            Ignore(r => r.Cliente);
            Ignore(r => r.Categoria);
        }
    }
}