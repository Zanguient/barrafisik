using BarraFisik.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace BarraFisik.Infra.Data.EntityConfig
{
    public class FornecedoresConfiguration : EntityTypeConfiguration<Fornecedores>
    {
        public FornecedoresConfiguration()
        {
            ToTable("Fornecedores");

            HasKey(f => f.FornecedorId);

            Property(f => f.Nome).IsRequired().HasMaxLength(150);
            Property(f => f.CpfCnpj).IsOptional().HasMaxLength(30);
            Property(f => f.RazaoSocial).IsOptional().HasMaxLength(150);
            Property(f => f.Email).IsOptional().HasMaxLength(100);
            Property(f => f.Telefone1).IsOptional().HasMaxLength(20);
            Property(f => f.Telefone2).IsOptional().HasMaxLength(20);
            Property(f => f.Celular).IsOptional().HasMaxLength(20);
            Property(f => f.Fax).IsOptional().HasMaxLength(20);
            Property(f => f.Cep).IsOptional().HasMaxLength(15);
            Property(f => f.Endereco).IsOptional().HasMaxLength(180);
            Property(f => f.Numero).IsOptional().HasMaxLength(10);
            Property(f => f.Bairro).IsOptional().HasMaxLength(50);
            Property(f => f.Cidade).IsOptional().HasMaxLength(100);
        }
    }
}
