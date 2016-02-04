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

            Property(d => d.Documento).IsOptional().HasMaxLength(100);
            Property(d => d.Valor).HasPrecision(10, 2);
            Property(d => d.Juros).HasPrecision(10, 2);
            Property(d => d.Multa).HasPrecision(10, 2);
            Property(d => d.ValorTotal).HasPrecision(10, 2);
            Property(d => d.Observacao).IsOptional().HasMaxLength(250);
            Property(d => d.Situacao).IsOptional().HasMaxLength(50);
            Property(d => d.Tipo).IsOptional().HasMaxLength(50);
            Property(l => l.CategoriaFinanceiraId).IsRequired();
            Property(d => d.ClienteId).IsOptional();
            Property(d => d.FornecedorId).IsOptional();
            Property(d => d.FuncionarioId).IsOptional();

            //ID
            Property(l => l.RegistroId).IsRequired();

            //Log
            Property(l => l.UserId).IsRequired();
            Property(l => l.UsuarioNome).IsRequired();
            Property(l => l.Acao).IsRequired();
        }
    }
}