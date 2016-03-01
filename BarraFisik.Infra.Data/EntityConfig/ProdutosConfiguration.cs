using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class ProdutosConfiguration : EntityTypeConfiguration<Produtos>
    {
        public ProdutosConfiguration()
        {
            ToTable("Produtos");

            HasKey(p => p.ProdutoId);

            Property(p => p.Nome).IsRequired();
            Property(p => p.Descricao).IsOptional().HasMaxLength(200);

            HasRequired(p => p.ProdutosCategoria).WithMany().HasForeignKey(p => p.ProdutoCategoriaId);
        }
    }
}