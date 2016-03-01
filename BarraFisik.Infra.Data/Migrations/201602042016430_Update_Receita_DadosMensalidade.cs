namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Receita_DadosMensalidade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receitas", "MesReferencia", c => c.Int());
            AddColumn("dbo.Receitas", "AnoReferencia", c => c.Int());
            AddColumn("dbo.Receitas", "isPersonal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Receitas", "ValorPersonal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Receitas", "ValorPersonal");
            DropColumn("dbo.Receitas", "isPersonal");
            DropColumn("dbo.Receitas", "AnoReferencia");
            DropColumn("dbo.Receitas", "MesReferencia");
        }
    }
}
