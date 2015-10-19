namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Despesas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Despesas",
                c => new
                    {
                        DespesasId = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Observacao = c.String(maxLength: 250, unicode: false),
                        CategoriaFinanceiraId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.DespesasId)
                .ForeignKey("dbo.CategoriaFinanceira", t => t.CategoriaFinanceiraId)
                .Index(t => t.CategoriaFinanceiraId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Despesas", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira");
            DropIndex("dbo.Despesas", new[] { "CategoriaFinanceiraId" });
            DropTable("dbo.Despesas");
        }
    }
}
