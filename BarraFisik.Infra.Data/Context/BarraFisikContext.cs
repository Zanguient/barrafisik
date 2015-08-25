using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BarraFisik.Domain.Entities;
using BarraFisik.Infra.Data.EntityConfig;

namespace BarraFisik.Infra.Data.Context
{
    public class BarraFisikContext : BaseContext
    {
        public BarraFisikContext() : base("BarraFisikConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();


            //General Custom Properties
            modelBuilder.Properties().Where(p => p.ReflectedType != null && p.Name == p.ReflectedType + "Id").Configure(p => p.IsKey());
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ClienteConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}