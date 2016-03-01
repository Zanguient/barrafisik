namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TipoPagamento_Despesa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Despesas", "TipoPagamentoId", c => c.Int(nullable: true));
            CreateIndex("dbo.Despesas", "TipoPagamentoId");
            AddForeignKey("dbo.Despesas", "TipoPagamentoId", "dbo.TipoPagamento", "TipoPagamentoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Despesas", "TipoPagamentoId", "dbo.TipoPagamento");
            DropIndex("dbo.Despesas", new[] { "TipoPagamentoId" });
            DropColumn("dbo.Despesas", "TipoPagamentoId");
        }
    }
}
