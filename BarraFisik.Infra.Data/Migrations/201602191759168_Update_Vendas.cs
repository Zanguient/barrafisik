namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Vendas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vendas", "EstoqueId", "dbo.Estoque");
            DropIndex("dbo.Vendas", new[] { "EstoqueId" });
            CreateTable(
                "dbo.VendasProdutos",
                c => new
                    {
                        VendasProdutosId = c.Guid(nullable: false),
                        VendaId = c.Guid(nullable: false),
                        EstoqueId = c.Guid(nullable: false),
                        Quantidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VendasProdutosId)
                .ForeignKey("dbo.Estoque", t => t.EstoqueId)
                .ForeignKey("dbo.Vendas", t => t.VendaId)
                .Index(t => t.VendaId)
                .Index(t => t.EstoqueId);
            
            AddColumn("dbo.Vendas", "ValorTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Vendas", "Descricao", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.Vendas", "EstoqueId");
            DropColumn("dbo.Vendas", "Quantidade");
            DropColumn("dbo.Vendas", "ValorPago");
            DropColumn("dbo.Vendas", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vendas", "Nome", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Vendas", "ValorPago", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.Vendas", "Quantidade", c => c.Int(nullable: false));
            AddColumn("dbo.Vendas", "EstoqueId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.VendasProdutos", "VendaId", "dbo.Vendas");
            DropForeignKey("dbo.VendasProdutos", "EstoqueId", "dbo.Estoque");
            DropIndex("dbo.VendasProdutos", new[] { "EstoqueId" });
            DropIndex("dbo.VendasProdutos", new[] { "VendaId" });
            DropColumn("dbo.Vendas", "Descricao");
            DropColumn("dbo.Vendas", "ValorTotal");
            DropTable("dbo.VendasProdutos");
            CreateIndex("dbo.Vendas", "EstoqueId");
            AddForeignKey("dbo.Vendas", "EstoqueId", "dbo.Estoque", "EstoqueId");
        }
    }
}
