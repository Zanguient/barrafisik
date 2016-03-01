namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_MovimentacaoEstoque_Estoque : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimentacaoEstoque", "EstoqueId", c => c.Guid(nullable: false));
            CreateIndex("dbo.MovimentacaoEstoque", "EstoqueId");
            AddForeignKey("dbo.MovimentacaoEstoque", "EstoqueId", "dbo.Estoque", "EstoqueId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovimentacaoEstoque", "EstoqueId", "dbo.Estoque");
            DropIndex("dbo.MovimentacaoEstoque", new[] { "EstoqueId" });
            DropColumn("dbo.MovimentacaoEstoque", "EstoqueId");
        }
    }
}
