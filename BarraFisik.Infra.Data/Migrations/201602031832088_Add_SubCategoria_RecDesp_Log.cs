namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SubCategoria_RecDesp_Log : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Despesas", "SubCategoriaFinanceiraId", c => c.Guid());
            AddColumn("dbo.LogReceitasDespesas", "SubCategoriaFinanceiraId", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Receitas", "SubCategoriaFinanceiraId", c => c.Guid());
            CreateIndex("dbo.Despesas", "SubCategoriaFinanceiraId");
            CreateIndex("dbo.Receitas", "SubCategoriaFinanceiraId");
            AddForeignKey("dbo.Despesas", "SubCategoriaFinanceiraId", "dbo.SubCategoriaFinanceira", "SubCategoriaFinanceiraId");
            AddForeignKey("dbo.Receitas", "SubCategoriaFinanceiraId", "dbo.SubCategoriaFinanceira", "SubCategoriaFinanceiraId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receitas", "SubCategoriaFinanceiraId", "dbo.SubCategoriaFinanceira");
            DropForeignKey("dbo.Despesas", "SubCategoriaFinanceiraId", "dbo.SubCategoriaFinanceira");
            DropIndex("dbo.Receitas", new[] { "SubCategoriaFinanceiraId" });
            DropIndex("dbo.Despesas", new[] { "SubCategoriaFinanceiraId" });
            DropColumn("dbo.Receitas", "SubCategoriaFinanceiraId");
            DropColumn("dbo.LogReceitasDespesas", "SubCategoriaFinanceiraId");
            DropColumn("dbo.Despesas", "SubCategoriaFinanceiraId");
        }
    }
}
