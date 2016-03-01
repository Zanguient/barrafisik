namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SubCategoriaFinanceira : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubCategoriaFinanceira",
                c => new
                    {
                        SubCategoriaFinanceiraId = c.Guid(nullable: false),
                        SubCategoria = c.String(nullable: false, maxLength: 100, unicode: false),
                        CategoriaFinanceiraId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SubCategoriaFinanceiraId)
                .ForeignKey("dbo.CategoriaFinanceira", t => t.CategoriaFinanceiraId)
                .Index(t => t.CategoriaFinanceiraId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategoriaFinanceira", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira");
            DropIndex("dbo.SubCategoriaFinanceira", new[] { "CategoriaFinanceiraId" });
            DropTable("dbo.SubCategoriaFinanceira");
        }
    }
}
