namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ProdutosCategoriaId_to_ProdutoCategoriaId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Produtos", name: "ProdutosCategoriaId", newName: "ProdutoCategoriaId");
            RenameIndex(table: "dbo.Produtos", name: "IX_ProdutosCategoriaId", newName: "IX_ProdutoCategoriaId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Produtos", name: "IX_ProdutoCategoriaId", newName: "IX_ProdutosCategoriaId");
            RenameColumn(table: "dbo.Produtos", name: "ProdutoCategoriaId", newName: "ProdutosCategoriaId");
        }
    }
}
