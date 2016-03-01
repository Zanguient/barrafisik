namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Produtos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        ProdutoId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Descricao = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.ProdutoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Produtos");
        }
    }
}
