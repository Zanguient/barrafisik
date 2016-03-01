namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SubCategoria_AvaliacaoFisica : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceitasAvaliacoesFisicas", "DataEmissao", c => c.DateTime(nullable: true));
            AddColumn("dbo.ReceitasAvaliacoesFisicas", "SubCategoriaFinanceiraId", c => c.Guid(nullable: true));
            CreateIndex("dbo.ReceitasAvaliacoesFisicas", "SubCategoriaFinanceiraId");
            AddForeignKey("dbo.ReceitasAvaliacoesFisicas", "SubCategoriaFinanceiraId", "dbo.SubCategoriaFinanceira", "SubCategoriaFinanceiraId");
            DropColumn("dbo.ReceitasAvaliacoesFisicas", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReceitasAvaliacoesFisicas", "Nome", c => c.String(maxLength: 100, unicode: false));
            DropForeignKey("dbo.ReceitasAvaliacoesFisicas", "SubCategoriaFinanceiraId", "dbo.SubCategoriaFinanceira");
            DropIndex("dbo.ReceitasAvaliacoesFisicas", new[] { "SubCategoriaFinanceiraId" });
            DropColumn("dbo.ReceitasAvaliacoesFisicas", "SubCategoriaFinanceiraId");
            DropColumn("dbo.ReceitasAvaliacoesFisicas", "DataEmissao");
        }
    }
}
