namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DataEmissao_Mensalidades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensalidades", "DataEmissao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mensalidades", "DataEmissao");
        }
    }
}
