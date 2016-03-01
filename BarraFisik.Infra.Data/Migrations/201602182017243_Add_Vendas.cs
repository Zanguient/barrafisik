namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Vendas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        VendaId = c.Guid(nullable: false),
                        ClienteId = c.Guid(),
                        TipoPagamentoId = c.Int(),
                        EstoqueId = c.Guid(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        ValorPago = c.Decimal(nullable: false, precision: 10, scale: 2),
                        DataVencimento = c.DateTime(nullable: false),
                        DataPagamento = c.DateTime(nullable: false),
                        ReceitasId = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.VendaId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Estoque", t => t.EstoqueId)
                .ForeignKey("dbo.Receitas", t => t.ReceitasId)
                .ForeignKey("dbo.TipoPagamento", t => t.TipoPagamentoId)
                .Index(t => t.ClienteId)
                .Index(t => t.TipoPagamentoId)
                .Index(t => t.EstoqueId)
                .Index(t => t.ReceitasId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendas", "TipoPagamentoId", "dbo.TipoPagamento");
            DropForeignKey("dbo.Vendas", "ReceitasId", "dbo.Receitas");
            DropForeignKey("dbo.Vendas", "EstoqueId", "dbo.Estoque");
            DropForeignKey("dbo.Vendas", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Vendas", new[] { "ReceitasId" });
            DropIndex("dbo.Vendas", new[] { "EstoqueId" });
            DropIndex("dbo.Vendas", new[] { "TipoPagamentoId" });
            DropIndex("dbo.Vendas", new[] { "ClienteId" });
            DropTable("dbo.Vendas");
        }
    }
}
