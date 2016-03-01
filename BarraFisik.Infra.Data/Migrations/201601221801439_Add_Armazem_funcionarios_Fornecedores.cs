namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Armazem_funcionarios_Fornecedores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Armazem",
                c => new
                    {
                        ArmazemId = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.ArmazemId);
            
            CreateTable(
                "dbo.Fornecedores",
                c => new
                    {
                        FornecedorId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        CpfCnpj = c.String(maxLength: 30, unicode: false),
                        RazaoSocial = c.String(maxLength: 150, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Telefone1 = c.String(maxLength: 20, unicode: false),
                        Telefone2 = c.String(maxLength: 20, unicode: false),
                        Celular = c.String(maxLength: 20, unicode: false),
                        Fax = c.String(maxLength: 20, unicode: false),
                        Cep = c.String(maxLength: 15, unicode: false),
                        Endereco = c.String(maxLength: 180, unicode: false),
                        Numero = c.String(maxLength: 10, unicode: false),
                        Bairro = c.String(maxLength: 50, unicode: false),
                        Cidade = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.FornecedorId);
            
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        FuncionarioId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        DataNascimento = c.DateTime(),
                        Cpf = c.String(maxLength: 20, unicode: false),
                        Telefone = c.String(maxLength: 20, unicode: false),
                        Celular = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 150, unicode: false),
                        Endereco = c.String(maxLength: 200, unicode: false),
                        Bairro = c.String(maxLength: 60, unicode: false),
                    })
                .PrimaryKey(t => t.FuncionarioId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Funcionarios");
            DropTable("dbo.Fornecedores");
            DropTable("dbo.Armazem");
        }
    }
}
