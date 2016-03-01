namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Vendas_Datas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendas", "DataVenda", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vendas", "DataPagamento", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vendas", "DataPagamento", c => c.DateTime(nullable: false));
            DropColumn("dbo.Vendas", "DataVenda");
        }
    }
}
