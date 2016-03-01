namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Update_Despesas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Despesas", "Documento", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Despesas", "DataPagamento", c => c.DateTime());
            AddColumn("dbo.Despesas", "DataVencimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Despesas", "Juros", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Despesas", "Multa", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Despesas", "ValorTotal", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Despesas", "Situacao", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Despesas", "FornecedorId", c => c.Guid());
            AddColumn("dbo.Despesas", "FuncionarioId", c => c.Guid());
            CreateIndex("dbo.Despesas", "FornecedorId");
            CreateIndex("dbo.Despesas", "FuncionarioId");
            AddForeignKey("dbo.Despesas", "FornecedorId", "dbo.Fornecedores", "FornecedorId");
            AddForeignKey("dbo.Despesas", "FuncionarioId", "dbo.Funcionarios", "FuncionarioId");
            DropColumn("dbo.Despesas", "Data");
            DropColumn("dbo.Despesas", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Despesas", "Nome", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.Despesas", "Data", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Despesas", "FuncionarioId", "dbo.Funcionarios");
            DropForeignKey("dbo.Despesas", "FornecedorId", "dbo.Fornecedores");
            DropIndex("dbo.Despesas", new[] { "FuncionarioId" });
            DropIndex("dbo.Despesas", new[] { "FornecedorId" });
            DropColumn("dbo.Despesas", "FuncionarioId");
            DropColumn("dbo.Despesas", "FornecedorId");
            DropColumn("dbo.Despesas", "Situacao");
            DropColumn("dbo.Despesas", "ValorTotal");
            DropColumn("dbo.Despesas", "Multa");
            DropColumn("dbo.Despesas", "Juros");
            DropColumn("dbo.Despesas", "DataVencimento");
            DropColumn("dbo.Despesas", "DataPagamento");
            DropColumn("dbo.Despesas", "Documento");
        }
    }
}
