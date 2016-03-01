namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProdutosCategoria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProdutosCategoria",
                c => new
                    {
                        ProdutoCategoriaId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Descricao = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.ProdutoCategoriaId);
            
            AddColumn("dbo.Produtos", "ProdutosCategoriaId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Produtos", "ProdutosCategoriaId");
            AddForeignKey("dbo.Produtos", "ProdutosCategoriaId", "dbo.ProdutosCategoria", "ProdutoCategoriaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtos", "ProdutosCategoriaId", "dbo.ProdutosCategoria");
            DropIndex("dbo.Produtos", new[] { "ProdutosCategoriaId" });
            DropColumn("dbo.Produtos", "ProdutosCategoriaId");
            DropTable("dbo.ProdutosCategoria");
        }
    }
}
