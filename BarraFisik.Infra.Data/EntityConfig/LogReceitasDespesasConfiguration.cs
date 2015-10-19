using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class LogReceitasDespesasConfiguration : EntityTypeConfiguration<LogReceitasDespesas>
    {
        public LogReceitasDespesasConfiguration()
        {
            ToTable("LogReceitasDespesas");

            HasKey(r => r.LogReceitasDespesasId);

            Property(l => l.Observacao).IsOptional().HasMaxLength(250);
            Property(l => l.Valor).HasPrecision(10, 2);
            Property(l => l.Nome).IsRequired();
            Property(l => l.CategoriaFinanceiraId).IsRequired();

            //ID
            Property(l => l.RegistroId).IsRequired();

            //Log
            Property(l => l.UserId).IsRequired();
            Property(l => l.UsuarioNome).IsRequired();
            Property(l => l.Acao).IsRequired();
        }
    }
}