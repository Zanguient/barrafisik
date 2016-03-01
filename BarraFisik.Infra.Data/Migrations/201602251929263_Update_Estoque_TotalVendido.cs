namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Estoque_TotalVendido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estoque", "TotalVendido", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Estoque", "TotalVendido");
        }
    }
}
