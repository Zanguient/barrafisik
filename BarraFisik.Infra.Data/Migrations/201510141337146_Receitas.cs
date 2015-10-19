namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Receitas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Receitas",
                c => new
                    {
                        ReceitasId = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 7, scale: 2),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Observacao = c.String(maxLength: 250, unicode: false),
                        CategoriaFinanceiraId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ReceitasId)
                .ForeignKey("dbo.CategoriaFinanceira", t => t.CategoriaFinanceiraId)
                .Index(t => t.CategoriaFinanceiraId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receitas", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira");
            DropIndex("dbo.Receitas", new[] { "CategoriaFinanceiraId" });
            DropTable("dbo.Receitas");
        }
    }
}
