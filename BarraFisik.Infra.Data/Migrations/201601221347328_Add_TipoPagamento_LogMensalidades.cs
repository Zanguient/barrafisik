namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TipoPagamento_LogMensalidades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogMensalidades", "TipoPagamentoId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogMensalidades", "TipoPagamentoId");
        }
    }
}
