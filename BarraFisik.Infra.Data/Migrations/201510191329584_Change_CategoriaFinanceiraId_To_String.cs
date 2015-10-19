namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_CategoriaFinanceiraId_To_String : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LogReceitasDespesas", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira");
            DropIndex("dbo.LogReceitasDespesas", new[] { "CategoriaFinanceiraId" });
            AlterColumn("dbo.LogReceitasDespesas", "CategoriaFinanceiraId", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LogReceitasDespesas", "CategoriaFinanceiraId", c => c.Guid(nullable: false));
            CreateIndex("dbo.LogReceitasDespesas", "CategoriaFinanceiraId");
            AddForeignKey("dbo.LogReceitasDespesas", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira", "CategoriaFinanceiraId");
        }
    }
}
