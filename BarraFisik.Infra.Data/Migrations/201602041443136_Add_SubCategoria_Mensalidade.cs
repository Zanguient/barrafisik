namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SubCategoria_Mensalidade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensalidades", "SubCategoriaFinanceiraId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Mensalidades", "SubCategoriaFinanceiraId");
            AddForeignKey("dbo.Mensalidades", "SubCategoriaFinanceiraId", "dbo.SubCategoriaFinanceira", "SubCategoriaFinanceiraId");
            DropColumn("dbo.Mensalidades", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mensalidades", "Nome", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropForeignKey("dbo.Mensalidades", "SubCategoriaFinanceiraId", "dbo.SubCategoriaFinanceira");
            DropIndex("dbo.Mensalidades", new[] { "SubCategoriaFinanceiraId" });
            DropColumn("dbo.Mensalidades", "SubCategoriaFinanceiraId");
        }
    }
}
