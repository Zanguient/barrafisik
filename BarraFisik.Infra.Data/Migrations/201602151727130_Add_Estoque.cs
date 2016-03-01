namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Estoque : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estoque",
                c => new
                    {
                        EstoqueId = c.Guid(nullable: false),
                        ArmazemId = c.Guid(nullable: false),
                        ProdutoId = c.Guid(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 10, scale: 2),
                        ValorTotal = c.Decimal(nullable: false, precision: 10, scale: 2),
                        SaldoVenda = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.EstoqueId)
                .ForeignKey("dbo.Armazem", t => t.ArmazemId)
                .ForeignKey("dbo.Produtos", t => t.ProdutoId)
                .Index(t => t.ArmazemId)
                .Index(t => t.ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estoque", "ProdutoId", "dbo.Produtos");
            DropForeignKey("dbo.Estoque", "ArmazemId", "dbo.Armazem");
            DropIndex("dbo.Estoque", new[] { "ProdutoId" });
            DropIndex("dbo.Estoque", new[] { "ArmazemId" });
            DropTable("dbo.Estoque");
        }
    }
}
