namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TipoPagamento_Mensalidades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensalidades", "TipoPagamentoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Mensalidades", "TipoPagamentoId");
            AddForeignKey("dbo.Mensalidades", "TipoPagamentoId", "dbo.TipoPagamento", "TipoPagamentoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensalidades", "TipoPagamentoId", "dbo.TipoPagamento");
            DropIndex("dbo.Mensalidades", new[] { "TipoPagamentoId" });
            DropColumn("dbo.Mensalidades", "TipoPagamentoId");
        }
    }
}
