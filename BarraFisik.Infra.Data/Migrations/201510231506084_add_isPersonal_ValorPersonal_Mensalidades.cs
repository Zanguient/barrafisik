namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_isPersonal_ValorPersonal_Mensalidades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensalidades", "isPersonal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Mensalidades", "ValorPersonal", c => c.Decimal(precision: 7, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mensalidades", "ValorPersonal");
            DropColumn("dbo.Mensalidades", "isPersonal");
        }
    }
}
