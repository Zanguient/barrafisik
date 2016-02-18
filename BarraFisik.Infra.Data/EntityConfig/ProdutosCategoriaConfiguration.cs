using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class ProdutosCategoriaConfiguration : EntityTypeConfiguration<ProdutosCategoria>
    {
        public ProdutosCategoriaConfiguration()
        {
            ToTable("ProdutosCategoria");

            HasKey(p => p.ProdutoCategoriaId);

            Property(p => p.Nome).IsRequired();
            Property(p => p.Descricao).IsOptional().HasMaxLength(200);
        }
    }
}