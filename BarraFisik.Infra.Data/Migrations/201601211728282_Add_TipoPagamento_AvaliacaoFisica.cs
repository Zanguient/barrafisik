namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TipoPagamento_AvaliacaoFisica : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceitasAvaliacoesFisicas", "TipoPagamentoId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReceitasAvaliacoesFisicas", "TipoPagamentoId");
            AddForeignKey("dbo.ReceitasAvaliacoesFisicas", "TipoPagamentoId", "dbo.TipoPagamento", "TipoPagamentoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceitasAvaliacoesFisicas", "TipoPagamentoId", "dbo.TipoPagamento");
            DropIndex("dbo.ReceitasAvaliacoesFisicas", new[] { "TipoPagamentoId" });
            DropColumn("dbo.ReceitasAvaliacoesFisicas", "TipoPagamentoId");
        }
    }
}
