namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_MovimentacaoEstoque : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovimentacaoEstoque", "DespesasId", "dbo.Despesas");
            DropForeignKey("dbo.MovimentacaoEstoque", "ReceitasId", "dbo.Receitas");
            DropIndex("dbo.MovimentacaoEstoque", new[] { "ReceitasId" });
            DropIndex("dbo.MovimentacaoEstoque", new[] { "DespesasId" });
            AddColumn("dbo.MovimentacaoEstoque", "ValorUnitario", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.MovimentacaoEstoque", "ValorTotalCusto", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.MovimentacaoEstoque", "DataCadastro", c => c.DateTime(nullable: false));
            DropColumn("dbo.MovimentacaoEstoque", "ValorUnVenda");
            DropColumn("dbo.MovimentacaoEstoque", "ValorTotal");
            DropColumn("dbo.MovimentacaoEstoque", "DataEmissao");
            DropColumn("dbo.MovimentacaoEstoque", "ReceitasId");
            DropColumn("dbo.MovimentacaoEstoque", "DespesasId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovimentacaoEstoque", "DespesasId", c => c.Guid());
            AddColumn("dbo.MovimentacaoEstoque", "ReceitasId", c => c.Guid());
            AddColumn("dbo.MovimentacaoEstoque", "DataEmissao", c => c.DateTime(nullable: false));
            AddColumn("dbo.MovimentacaoEstoque", "ValorTotal", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.MovimentacaoEstoque", "ValorUnVenda", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            DropColumn("dbo.MovimentacaoEstoque", "DataCadastro");
            DropColumn("dbo.MovimentacaoEstoque", "ValorTotalCusto");
            DropColumn("dbo.MovimentacaoEstoque", "ValorUnitario");
            CreateIndex("dbo.MovimentacaoEstoque", "DespesasId");
            CreateIndex("dbo.MovimentacaoEstoque", "ReceitasId");
            AddForeignKey("dbo.MovimentacaoEstoque", "ReceitasId", "dbo.Receitas", "ReceitasId");
            AddForeignKey("dbo.MovimentacaoEstoque", "DespesasId", "dbo.Despesas", "DespesasId");
        }
    }
}
