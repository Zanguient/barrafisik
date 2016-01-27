using BarraFisik.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class FuncionariosConfiguration : EntityTypeConfiguration<Funcionarios>
    {
        public FuncionariosConfiguration()
        {
            ToTable("Funcionarios");

            HasKey(f => f.FuncionarioId);

            Property(f => f.Nome).IsRequired().HasMaxLength(150);
            Property(f => f.Endereco).IsOptional().HasMaxLength(200);
            Property(f => f.Cpf).IsOptional().HasMaxLength(20);
            Property(f => f.Telefone).IsOptional().HasMaxLength(20);
            Property(f => f.Celular).IsOptional().HasMaxLength(20);
            Property(f => f.Email).IsOptional().HasMaxLength(150);
            Property(f => f.Bairro).IsOptional().HasMaxLength(60);
        }
    }
}
