using BarraFisik.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class MovimentacaoEstoqueConfiguration : EntityTypeConfiguration<MovimentacaoEstoque>
    {
        public MovimentacaoEstoqueConfiguration()
        {
            ToTable("MovimentacaoEstoque");

            HasKey(e => e.MovimentacaoId);

            Property(e => e.ValorUnCusto).HasPrecision(10, 2);
            Property(e => e.ValorTotalCusto).HasPrecision(10, 2);
            Property(e => e.ValorUnitario).HasPrecision(10, 2);
            Property(e => e.TipoMovimento).HasMaxLength(50);

            HasRequired(e => e.Produtos).WithMany().HasForeignKey(e => e.ProdutoId);
            HasRequired(e => e.Armazem).WithMany().HasForeignKey(e => e.ArmazemId);
            HasRequired(e => e.Estoque).WithMany().HasForeignKey(e => e.EstoqueId);
        }
    }
}
