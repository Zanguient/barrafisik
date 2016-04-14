namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Funcionario_Vendas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendas", "FuncionarioId", c => c.Guid());
            CreateIndex("dbo.Vendas", "FuncionarioId");
            AddForeignKey("dbo.Vendas", "FuncionarioId", "dbo.Funcionarios", "FuncionarioId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendas", "FuncionarioId", "dbo.Funcionarios");
            DropIndex("dbo.Vendas", new[] { "FuncionarioId" });
            DropColumn("dbo.Vendas", "FuncionarioId");
        }
    }
}
