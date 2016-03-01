using BarraFisik.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class EstoqueConfiguration : EntityTypeConfiguration<Estoque>
    {
        public EstoqueConfiguration()
        {
            ToTable("Estoque");

            HasKey(e => e.EstoqueId);

            Property(e => e.ValorUnitario).HasPrecision(10, 2);
            Property(e => e.ValorTotal).HasPrecision(10, 2);
            Property(e => e.SaldoVenda).HasPrecision(10, 2);

            HasRequired(e => e.Produtos).WithMany().HasForeignKey(e => e.ProdutoId);
            HasRequired(e => e.Armazem).WithMany().HasForeignKey(e => e.ArmazemId);
        }
    }
}
