namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_isAtivo_Fornecedores : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fornecedores", "isAtivo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fornecedores", "isAtivo");
        }
    }
}
