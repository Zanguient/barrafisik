using System.Data.Entity;
using BarraFisik.Infra.CrossCutting.Identity.Infra;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BarraFisik.Infra.CrossCutting.Identity.Context
{
    public class AspNetIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AspNetIdentityDbContext() : base("BarraFisikConnectionString")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static AspNetIdentityDbContext Create()
        {
            return new AspNetIdentityDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //General custom properties
        //    modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
        //    modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
