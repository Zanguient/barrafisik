using System;
using System.Data.Entity.Migrations;
using BarraFisik.Domain.Entities;
using BarraFisik.Infra.Data.Context;

namespace BarraFisik.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BarraFisikContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BarraFisikContext context)
        {
            context.CategoriaFinanceira.AddOrUpdate(
                c => c.Categoria,
                new CategoriaFinanceira { CategoriaFinanceiraId = new Guid("1c1278df-f5a5-4407-a0c4-bdbb71c362b1"), Categoria = "Serviços", Tipo = "Receitas" }
            );

            context.SubCategoriaFinanceira.AddOrUpdate(c => c.SubCategoria,
                new SubCategoriaFinanceira
                {
                    SubCategoriaFinanceiraId = new Guid("0d57c87d-3bd9-420b-ab98-123fdb75a269"),
                    CategoriaFinanceiraId = new Guid("1c1278df-f5a5-4407-a0c4-bdbb71c362b1"),
                    SubCategoria = "Mensalidades"
                });

            context.SubCategoriaFinanceira.AddOrUpdate(c => c.SubCategoria,
                new SubCategoriaFinanceira
                {
                    SubCategoriaFinanceiraId = new Guid("ecaac024-15bd-4ee0-8422-07d809bb1be9"),
                    CategoriaFinanceiraId = new Guid("1c1278df-f5a5-4407-a0c4-bdbb71c362b1"),
                    SubCategoria = "Avaliação Física"
                });

        }
    }
}