namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_Horario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Horario",
                c => new
                    {
                        HorarioId = c.Guid(nullable: false),
                        Segunda = c.Boolean(nullable: false),
                        HSegunda = c.String(maxLength: 20, unicode: false),
                        Terca = c.Boolean(nullable: false),
                        HTerca = c.String(maxLength: 20, unicode: false),
                        Quarta = c.Boolean(nullable: false),
                        HQuarta = c.String(maxLength: 20, unicode: false),
                        Quinta = c.Boolean(nullable: false),
                        HQuinta = c.String(maxLength: 20, unicode: false),
                        Sexta = c.Boolean(nullable: false),
                        HSexta = c.String(maxLength: 20, unicode: false),
                        ClienteId = c.Guid(),
                    })
                .PrimaryKey(t => t.HorarioId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Horario", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Horario", new[] { "ClienteId" });
            DropTable("dbo.Horario");
        }
    }
}
