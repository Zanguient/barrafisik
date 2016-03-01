namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TipoPagamento_LogRecDesp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogReceitasDespesas", "TipoPagamentoId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogReceitasDespesas", "TipoPagamentoId");
        }
    }
}
