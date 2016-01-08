using System.Data.Entity.ModelConfiguration;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            ToTable("Cliente");

            HasKey(c => c.ClienteId);

            Property(c => c.Nome).IsRequired();
            Property(c => c.Cpf).IsOptional().HasMaxLength(20);
            Property(c => c.Endereco).IsRequired().HasMaxLength(200);
            Property(c => c.DtNascimento).IsRequired();
            Property(c => c.DtInscricao).IsRequired();

            Property(c => c.Telefone).IsOptional().HasMaxLength(15);
            Property(c => c.Celular).IsOptional().HasMaxLength(15);
            Property(c => c.Email).IsOptional();
            Property(c => c.Rg).IsOptional().HasMaxLength(20);
            Property(c => c.Sexo).IsOptional().HasMaxLength(1).HasColumnType("char");
            Property(c => c.QtdFilhos).IsOptional();
            Property(c => c.Path).IsOptional().HasMaxLength(250);
            Property(c => c.Situacao).IsOptional();

            Ignore(t => t.ResultadoValidacao);

            HasOptional(c => c.Valores);

        }
    }
}