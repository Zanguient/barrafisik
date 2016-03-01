namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Tipo_LogRecDesp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogReceitasDespesas", "Tipo", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogReceitasDespesas", "Tipo");
        }
    }
}
