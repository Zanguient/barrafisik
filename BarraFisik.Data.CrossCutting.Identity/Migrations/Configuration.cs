using BarraFisik.Data.CrossCutting.Identity.Context;
using BarraFisik.Data.CrossCutting.Identity.Infra;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BarraFisik.Data.CrossCutting.Identity.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspNetIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AspNetIdentityDbContext context)
        {            
        }
    }
}
