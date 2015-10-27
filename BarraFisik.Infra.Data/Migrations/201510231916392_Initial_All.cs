namespace BarraFisik.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_All : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaFinanceira",
                c => new
                    {
                        CategoriaFinanceiraId = c.Guid(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Categoria = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.CategoriaFinanceiraId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Endereco = c.String(nullable: false, maxLength: 200, unicode: false),
                        Telefone = c.String(maxLength: 15, unicode: false),
                        Celular = c.String(maxLength: 15, unicode: false),
                        DtNascimento = c.DateTime(nullable: false),
                        DtInscricao = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Cpf = c.String(nullable: false, maxLength: 20, unicode: false),
                        Rg = c.String(maxLength: 20, unicode: false),
                        Sexo = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        QtdFilhos = c.Int(),
                        IsAtivo = c.Boolean(nullable: false),
                        Path = c.String(maxLength: 250, unicode: false),
                        Situacao = c.String(maxLength: 100, unicode: false),
                        ValoresId = c.Guid(),
                    })
                .PrimaryKey(t => t.ClienteId)
                .ForeignKey("dbo.Valores", t => t.ValoresId)
                .Index(t => t.ValoresId);
            
            CreateTable(
                "dbo.Mensalidades",
                c => new
                    {
                        MensalidadesId = c.Guid(nullable: false),
                        ValorPago = c.Decimal(nullable: false, precision: 7, scale: 2),
                        MesReferencia = c.Int(nullable: false),
                        AnoReferencia = c.Int(nullable: false),
                        ClienteId = c.Guid(nullable: false),
                        DataPagamento = c.DateTime(nullable: false),
                        CategoriaFinanceiraId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        isPersonal = c.Boolean(nullable: false),
                        ValorPersonal = c.Decimal(precision: 7, scale: 2),
                    })
                .PrimaryKey(t => t.MensalidadesId)
                .ForeignKey("dbo.CategoriaFinanceira", t => t.CategoriaFinanceiraId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId)
                .Index(t => t.CategoriaFinanceiraId);
            
            CreateTable(
                "dbo.Valores",
                c => new
                    {
                        ValoresId = c.Guid(nullable: false),
                        QtdDias = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 6, scale: 2),
                        HorarioInicio = c.Int(nullable: false),
                        HorarioFim = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ValoresId);
            
            CreateTable(
                "dbo.Despesas",
                c => new
                    {
                        DespesasId = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Observacao = c.String(maxLength: 250, unicode: false),
                        CategoriaFinanceiraId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.DespesasId)
                .ForeignKey("dbo.CategoriaFinanceira", t => t.CategoriaFinanceiraId)
                .Index(t => t.CategoriaFinanceiraId);
            
            CreateTable(
                "dbo.FilaEspera",
                c => new
                    {
                        FilaEsperaId = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Telefone = c.String(maxLength: 15, unicode: false),
                        Celular = c.String(maxLength: 15, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        DataReserva = c.DateTime(),
                        Hora = c.Int(),
                    })
                .PrimaryKey(t => t.FilaEsperaId);
            
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
                        ClienteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.HorarioId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.LogSistema",
                c => new
                    {
                        LogSistemaId = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                        UsuarioNome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Acao = c.String(nullable: false, maxLength: 100, unicode: false),
                        Tabela = c.String(nullable: false, maxLength: 100, unicode: false),
                        RegistroId = c.Guid(nullable: false),
                        Descricao = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.LogSistemaId);
            
            CreateTable(
                "dbo.LogMensalidades",
                c => new
                    {
                        LogMensalidadesId = c.Guid(nullable: false),
                        MensalidadesId = c.Guid(nullable: false),
                        ValorPago = c.Decimal(nullable: false, precision: 6, scale: 2),
                        MesReferencia = c.Int(nullable: false),
                        AnoReferencia = c.Int(nullable: false),
                        ClienteId = c.Guid(nullable: false),
                        DataPagamento = c.DateTime(nullable: false),
                        Acao = c.String(nullable: false, maxLength: 100, unicode: false),
                        Data = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                        UsuarioNome = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.LogMensalidadesId);
            
            CreateTable(
                "dbo.LogReceitasDespesas",
                c => new
                    {
                        LogReceitasDespesasId = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Observacao = c.String(maxLength: 250, unicode: false),
                        CategoriaFinanceiraId = c.String(nullable: false, maxLength: 100, unicode: false),
                        RegistroId = c.String(nullable: false, maxLength: 100, unicode: false),
                        Acao = c.String(nullable: false, maxLength: 100, unicode: false),
                        DataAcao = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 100, unicode: false),
                        UsuarioNome = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.LogReceitasDespesasId);
            
            CreateTable(
                "dbo.Receitas",
                c => new
                    {
                        ReceitasId = c.Guid(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Observacao = c.String(maxLength: 250, unicode: false),
                        CategoriaFinanceiraId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ReceitasId)
                .ForeignKey("dbo.CategoriaFinanceira", t => t.CategoriaFinanceiraId)
                .Index(t => t.CategoriaFinanceiraId);
            
            CreateTable(
                "dbo.ReceitasAvaliacoesFisicas",
                c => new
                    {
                        ReceitasAvaliacaoFisicaId = c.Guid(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 5, scale: 2),
                        ClienteId = c.Guid(nullable: false),
                        DataPagamento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReceitasAvaliacaoFisicaId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceitasAvaliacoesFisicas", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Receitas", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira");
            DropForeignKey("dbo.Horario", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Despesas", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira");
            DropForeignKey("dbo.Cliente", "ValoresId", "dbo.Valores");
            DropForeignKey("dbo.Mensalidades", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Mensalidades", "CategoriaFinanceiraId", "dbo.CategoriaFinanceira");
            DropIndex("dbo.ReceitasAvaliacoesFisicas", new[] { "ClienteId" });
            DropIndex("dbo.Receitas", new[] { "CategoriaFinanceiraId" });
            DropIndex("dbo.Horario", new[] { "ClienteId" });
            DropIndex("dbo.Despesas", new[] { "CategoriaFinanceiraId" });
            DropIndex("dbo.Mensalidades", new[] { "CategoriaFinanceiraId" });
            DropIndex("dbo.Mensalidades", new[] { "ClienteId" });
            DropIndex("dbo.Cliente", new[] { "ValoresId" });
            DropTable("dbo.ReceitasAvaliacoesFisicas");
            DropTable("dbo.Receitas");
            DropTable("dbo.LogReceitasDespesas");
            DropTable("dbo.LogMensalidades");
            DropTable("dbo.LogSistema");
            DropTable("dbo.Horario");
            DropTable("dbo.FilaEspera");
            DropTable("dbo.Despesas");
            DropTable("dbo.Valores");
            DropTable("dbo.Mensalidades");
            DropTable("dbo.Cliente");
            DropTable("dbo.CategoriaFinanceira");
        }
    }
}
