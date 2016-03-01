namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTipoPagamento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoPagamento",
                c => new
                    {
                        TipoPagamentoId = c.Int(nullable: false, identity: true),
                        Sigla = c.String(nullable: false, maxLength: 5, unicode: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.TipoPagamentoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TipoPagamento");
        }
    }
}
