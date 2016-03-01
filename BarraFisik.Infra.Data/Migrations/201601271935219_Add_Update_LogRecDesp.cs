namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Update_LogRecDesp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogReceitasDespesas", "Documento", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.LogReceitasDespesas", "DataPagamento", c => c.DateTime());
            AddColumn("dbo.LogReceitasDespesas", "DataVencimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.LogReceitasDespesas", "Juros", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.LogReceitasDespesas", "Multa", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.LogReceitasDespesas", "ValorTotal", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddColumn("dbo.LogReceitasDespesas", "Situacao", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.LogReceitasDespesas", "FornecedorId", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.LogReceitasDespesas", "FuncionarioId", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.LogReceitasDespesas", "ClienteId", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.LogReceitasDespesas", "Data");
            DropColumn("dbo.LogReceitasDespesas", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LogReceitasDespesas", "Nome", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.LogReceitasDespesas", "Data", c => c.DateTime(nullable: false));
            DropColumn("dbo.LogReceitasDespesas", "ClienteId");
            DropColumn("dbo.LogReceitasDespesas", "FuncionarioId");
            DropColumn("dbo.LogReceitasDespesas", "FornecedorId");
            DropColumn("dbo.LogReceitasDespesas", "Situacao");
            DropColumn("dbo.LogReceitasDespesas", "ValorTotal");
            DropColumn("dbo.LogReceitasDespesas", "Multa");
            DropColumn("dbo.LogReceitasDespesas", "Juros");
            DropColumn("dbo.LogReceitasDespesas", "DataVencimento");
            DropColumn("dbo.LogReceitasDespesas", "DataPagamento");
            DropColumn("dbo.LogReceitasDespesas", "Documento");
        }
    }
}
