namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriaFinanceira : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaFinanceira",
                c => new
                    {
                        CategoriaFinanceiraId = c.Guid(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Categoria = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.CategoriaFinanceiraId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CategoriaFinanceira");
        }
    }
}
