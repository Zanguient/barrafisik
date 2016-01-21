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
        public IDbSet<Horario> Horarios { get; set; }
        public IDbSet<FilaEspera> FilaEspera { get; set; }
        public IDbSet<Mensalidades> Mensalidades { get; set; }
        public IDbSet<Valores> Valores { get; set; }
        public IDbSet<LogSistema> Log { get; set; }
        public IDbSet<LogMensalidades> LogMensalidades { get; set; }
        public IDbSet<ReceitasAvaliacaoFisica> ReceitasAvaliacaoFisica { get; set; }
        public IDbSet<CategoriaFinanceira> CategoriaFinanceira { get; set; }
        public IDbSet<Receitas> Receitas { get; set; }
        public IDbSet<Despesas> Despesas { get; set; }
        public IDbSet<LogReceitasDespesas> LogReceitasDespesas { get; set; }
        public IDbSet<TipoPagamento> TipoPagamento { get; set; }

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


            //Model Configuration
            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new HorarioConfiguration());
            modelBuilder.Configurations.Add(new FilaEsperaConfiguration());
            modelBuilder.Configurations.Add(new MensalidadesConfiguration());
            modelBuilder.Configurations.Add(new ValoresConfiguration());
            modelBuilder.Configurations.Add(new LogSistemaConfiguration());
            modelBuilder.Configurations.Add(new LogMensalidadesConfiguration());
            modelBuilder.Configurations.Add(new ReceitasAvaliacaoFisicaConfiguration());
            modelBuilder.Configurations.Add(new CategoriaFinanceiraConfiguration());
            modelBuilder.Configurations.Add(new ReceitasConfiguration());
            modelBuilder.Configurations.Add(new DespesasConfiguration());
            modelBuilder.Configurations.Add(new LogReceitasDespesasConfiguration());
            modelBuilder.Configurations.Add(new TipoPagamentoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}