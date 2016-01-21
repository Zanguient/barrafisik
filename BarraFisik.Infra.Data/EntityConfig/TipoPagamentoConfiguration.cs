using BarraFisik.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class TipoPagamentoConfiguration : EntityTypeConfiguration<TipoPagamento>
    {
        public TipoPagamentoConfiguration()
        {
            ToTable("TipoPagamento");

            HasKey(c => c.TipoPagamentoId);

            Property(c => c.Sigla).IsRequired().HasMaxLength(5);
            Property(c => c.Descricao).IsRequired().HasMaxLength(50);
        }
    }
}
