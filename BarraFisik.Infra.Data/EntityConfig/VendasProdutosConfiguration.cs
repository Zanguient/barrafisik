using BarraFisik.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class VendasProdutosConfiguration : EntityTypeConfiguration<VendasProdutos>
    {
        public VendasProdutosConfiguration()
        {
            ToTable("VendasProdutos");

            HasKey(c => c.VendasProdutosId);

            HasRequired(c => c.Estoque).WithMany().HasForeignKey(c => c.EstoqueId);
            HasRequired(c => c.Vendas).WithMany().HasForeignKey(c => c.VendaId);

        }
    }
}
