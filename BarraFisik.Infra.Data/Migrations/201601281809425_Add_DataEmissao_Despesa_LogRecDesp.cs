namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DataEmissao_Despesa_LogRecDesp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Despesas", "DataEmissao", c => c.DateTime(nullable: false));
            AddColumn("dbo.LogReceitasDespesas", "DataEmissao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogReceitasDespesas", "DataEmissao");
            DropColumn("dbo.Despesas", "DataEmissao");
        }
    }
}
