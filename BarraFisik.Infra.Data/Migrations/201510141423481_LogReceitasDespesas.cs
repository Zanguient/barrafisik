namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogReceitasDespesas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogReceitasDespesas",
                c => new
                    {
                        LogReceitasDespesasId = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 5, scale: 2),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Observacao = c.String(maxLength: 250, unicode: false),
                        CategoriaFinanceiraId = c.Guid(nullable: false),
                        RegistroId = c.Guid(nullable: false),
                        Acao = c.String(nullable: false, maxLength: 100, unicode: false),
                        DataAcao = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                        UsuarioNome = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.LogReceitasDespesasId)
                .ForeignKey("dbo.CategoriaFinanceira", t => t.CategoriaFinanceiraId)
                .Index(t => t.CategoriaFinanceiraId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogReceitasDespesas", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira");
            DropIndex("dbo.LogReceitasDespesas", new[] { "CategoriaFinanceiraId" });
            DropTable("dbo.LogReceitasDespesas");
        }
    }
}
