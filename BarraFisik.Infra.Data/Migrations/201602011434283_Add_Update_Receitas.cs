namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Update_Receitas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receitas", "Documento", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Receitas", "DataPagamento", c => c.DateTime());
            AddColumn("dbo.Receitas", "DataVencimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Receitas", "DataEmissao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Receitas", "Juros", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Receitas", "Multa", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Receitas", "ValorTotal", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Receitas", "Situacao", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Receitas", "CpfCnpj", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Receitas", "TipoPagamentoId", c => c.Int());
            AddColumn("dbo.Receitas", "FuncionarioId", c => c.Guid());
            AddColumn("dbo.Receitas", "ClienteId", c => c.Guid());
            AlterColumn("dbo.Receitas", "Nome", c => c.String(maxLength: 100, unicode: false));
            CreateIndex("dbo.Receitas", "TipoPagamentoId");
            CreateIndex("dbo.Receitas", "FuncionarioId");
            CreateIndex("dbo.Receitas", "ClienteId");
            AddForeignKey("dbo.Receitas", "ClienteId", "dbo.Cliente", "ClienteId");
            AddForeignKey("dbo.Receitas", "FuncionarioId", "dbo.Funcionarios", "FuncionarioId");
            AddForeignKey("dbo.Receitas", "TipoPagamentoId", "dbo.TipoPagamento", "TipoPagamentoId");
            DropColumn("dbo.Receitas", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Receitas", "Data", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Receitas", "TipoPagamentoId", "dbo.TipoPagamento");
            DropForeignKey("dbo.Receitas", "FuncionarioId", "dbo.Funcionarios");
            DropForeignKey("dbo.Receitas", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Receitas", new[] { "ClienteId" });
            DropIndex("dbo.Receitas", new[] { "FuncionarioId" });
            DropIndex("dbo.Receitas", new[] { "TipoPagamentoId" });
            AlterColumn("dbo.Receitas", "Nome", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropColumn("dbo.Receitas", "ClienteId");
            DropColumn("dbo.Receitas", "FuncionarioId");
            DropColumn("dbo.Receitas", "TipoPagamentoId");
            DropColumn("dbo.Receitas", "CpfCnpj");
            DropColumn("dbo.Receitas", "Situacao");
            DropColumn("dbo.Receitas", "ValorTotal");
            DropColumn("dbo.Receitas", "Multa");
            DropColumn("dbo.Receitas", "Juros");
            DropColumn("dbo.Receitas", "DataEmissao");
            DropColumn("dbo.Receitas", "DataVencimento");
            DropColumn("dbo.Receitas", "DataPagamento");
            DropColumn("dbo.Receitas", "Documento");
        }
    }
}
