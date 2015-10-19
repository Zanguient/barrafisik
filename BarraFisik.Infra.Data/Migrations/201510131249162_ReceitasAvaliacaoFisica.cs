namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceitasAvaliacaoFisica : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReceitasAvaliacoesFisicas",
                c => new
                    {
                        ReceitasAvaliacaoFisicaId = c.Guid(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 5, scale: 2),
                        ClienteId = c.Guid(nullable: false),
                        DataPagamento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReceitasAvaliacaoFisicaId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceitasAvaliacoesFisicas", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.ReceitasAvaliacoesFisicas", new[] { "ClienteId" });
            DropTable("dbo.ReceitasAvaliacoesFisicas");
        }
    }
}
