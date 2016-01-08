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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BarraFisikContext context)
        {
            //context.CategoriaFinanceira.AddOrUpdate(
            //    c => c.Categoria, 
            //    new CategoriaFinanceira { CategoriaFinanceiraId = new Guid("1c1278df-f5a5-4407-a0c4-bdbb71c362b1") ,Categoria = "Serviços", Tipo = "Receitas"}
            //);            
        }
    }
}