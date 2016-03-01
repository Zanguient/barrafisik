using BarraFisik.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class VendasConfiguration : EntityTypeConfiguration<Vendas>
    {
        public VendasConfiguration()
        {
            ToTable("Vendas");

            HasKey(v => v.VendaId);
        }
    }
}
