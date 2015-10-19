using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class ReceitasAvaliacaoFisicaConfiguration : EntityTypeConfiguration<ReceitasAvaliacaoFisica>
    {
        public ReceitasAvaliacaoFisicaConfiguration()
        {
            ToTable("ReceitasAvaliacoesFisicas");

            HasKey(r => r.ReceitasAvaliacaoFisicaId);

            Property(r => r.Valor).HasPrecision(5, 2);

            HasRequired(r => r.Cliente).WithMany().HasForeignKey(r => r.ClienteId);
        }
    }
}
