namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoria_Mensalidade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensalidades", "CategoriaFinanceiraId", c => c.Guid(nullable: false));
            AddColumn("dbo.Mensalidades", "Nome", c => c.String(nullable: false, maxLength: 100, unicode: false));
            CreateIndex("dbo.Mensalidades", "CategoriaFinanceiraId");
            AddForeignKey("dbo.Mensalidades", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira", "CategoriaFinanceiraId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensalidades", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira");
            DropIndex("dbo.Mensalidades", new[] { "CategoriaFinanceiraId" });
            DropColumn("dbo.Mensalidades", "Nome");
            DropColumn("dbo.Mensalidades", "CategoriaFinanceiraId");
        }
    }
}
