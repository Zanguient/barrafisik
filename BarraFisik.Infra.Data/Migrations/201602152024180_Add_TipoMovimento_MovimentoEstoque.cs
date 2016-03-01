namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TipoMovimento_MovimentoEstoque : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimentacaoEstoque", "TipoMovimento", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovimentacaoEstoque", "TipoMovimento");
        }
    }
}
