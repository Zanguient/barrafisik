using BarraFisik.Application.App;
using BarraFisik.Application.Interfaces;
using BarraFisik.Domain.Interfaces.Repository;
using BarraFisik.Domain.Interfaces.Repository.ReadOnly;
using BarraFisik.Domain.Interfaces.Services;
using BarraFisik.Domain.Services;
using BarraFisik.Infra.Data.Context;
using BarraFisik.Infra.Data.Interfaces;
using BarraFisik.Infra.Data.Repository;
using BarraFisik.Infra.Data.Repository.ReadOnly;
using BarraFisik.Infra.Data.UoW;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using UnityServiceLocator = Microsoft.Practices.Unity.ServiceLocatorAdapter.UnityServiceLocator;

namespace BarraFisik.Infra.CrossCutting.IoC.Unity
{
    public class Container
    {
        public Container()
        {
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(GetModule()));
        }

        public UnityContainer GetModule()
        {
            return ConfigureUnityContainer();
        }

        public UnityContainer ConfigureUnityContainer()
        {
            var container = new UnityContainer();

            //APP            
            container.RegisterType<IClienteAppService, ClienteAppService>();
            container.RegisterType<IHorarioAppService, HorarioAppService>();
            container.RegisterType<IFilaEsperaAppService, FilaEsperaAppService>();
            container.RegisterType<IMensalidadesAppService, MensalidadesAppService>();
            container.RegisterType<IValoresAppService, ValoresAppService>();
            container.RegisterType<IReceitasAvaliacaoFisicaAppService, ReceitasAvaliacaoFisicaAppService>();
            container.RegisterType<ICategoriaFinanceiraAppService, CategoriaFinanceiraAppService>();
            container.RegisterType<ISubCategoriaFinanceiraAppService, SubCategoriaFinanceiraAppService>();
            container.RegisterType<IReceitasAppService, ReceitasAppService>();
            container.RegisterType<IDespesasAppService, DespesasAppService>();
            container.RegisterType<IRelatorioFinanceiroAppService, RelatorioFinanceiroAppService>();
            container.RegisterType<ITipoPagamentoAppService, TipoPagamentoAppService>();
            container.RegisterType<IArmazemAppService, ArmazemAppService>();
            container.RegisterType<IFuncionariosAppService, FuncionariosAppService>();
            container.RegisterType<IFornecedoresAppService, FornecedoresAppService>();

            //Services
            container.RegisterType(typeof (IServiceBase<>), typeof (ServiceBase<>));
            container.RegisterType<IClienteService, ClienteService>();
            container.RegisterType<IHorarioService, HorarioService>();
            container.RegisterType<IFilaEsperaService, FilaEsperaService>();
            container.RegisterType<IMensalidadesService, MensalidadesService>();
            container.RegisterType<IValoresService, ValoresService>();
            container.RegisterType<ILogSistemaService, LogSistemaService>();
            container.RegisterType<ILogReceitasDespesasService, LogReceitasDespesasService>();
            container.RegisterType<ILogMensalidadesService, LogMensalidadesService>();
            container.RegisterType<IReceitasAvaliacaoFisicaService, ReceitasAvaliacaoFisicaService>();
            container.RegisterType<ICategoriaFinanceiraService, CategoriaFinanceiraService>();
            container.RegisterType<ISubCategoriaFinanceiraService, SubCategoriaFinanceiraService>();
            container.RegisterType<IReceitasService, ReceitasService>();
            container.RegisterType<IDespesasService, DespesasService>();
            container.RegisterType<IRelatorioFinanceiroService, RelatorioFinanceiroService>();
            container.RegisterType<ITipoPagamentoService, TipoPagamentoService>();
            container.RegisterType<IArmazemService, ArmazemService>();
            container.RegisterType<IFuncionariosService, FuncionariosService>();
            container.RegisterType<IFornecedoresService, FornecedoresService>();

            //Data Repos
            container.RegisterType(typeof (IRepositoryBase<>), typeof (RepositoryBase<,>));
            container.RegisterType<IClienteRepository, ClienteRepository>();
            container.RegisterType<IHorarioRepository, HorarioRepository>();
            container.RegisterType<IFilaEsperaRepository, FilaEsperaRepository>();
            container.RegisterType<IMensalidadesRepository, MensalidadesRepository>();
            container.RegisterType<IValoresRepository, ValoresRepository>();
            container.RegisterType<ILogSistemaRepository, LogSistemaRepository>();
            container.RegisterType<ILogReceitasDespesasRepository, LogReceitasDespesasRepository>();
            container.RegisterType<ILogMensalidadesRepository, LogMensalidadesRepository>();
            container.RegisterType<IReceitasAvaliacaoFisicaRepository, ReceitasAvaliacaoFisicaRepository>();
            container.RegisterType<ICategoriaFinanceiraRepository, CategoriaFinanceiraRepository>();
            container.RegisterType<ISubCategoriaFinanceiraRepository, SubCategoriaFinanceiraRepository>();
            container.RegisterType<IReceitasRepository, ReceitasRepository>();
            container.RegisterType<IDespesasRepository, DespesasRepository>();
            container.RegisterType<IRelatorioFinanceiroRepository, RelatorioFinanceiroRepository>();
            container.RegisterType<ITipoPagamentoRepository, TipoPagamentoRepository>();
            container.RegisterType<IArmazemRepository, ArmazemRepository>();
            container.RegisterType<IFuncionariosRepository, FuncionariosRepository>();
            container.RegisterType<IFornecedoresRepository, FornecedoresRepository>();

            //Data Repos Read Only
            container.RegisterType<IClienteRepositoryReadOnly, ClienteRepositoryReadOnly>();
            container.RegisterType<IHorarioRepositoryReadOnly, HorarioRepositoryReadOnly>();
            container.RegisterType<IMensalidadesRepositoryReadOnly, MensalidadesRepositoryReadOnly>();
            container.RegisterType<IValoresRepositoryReadOnly, ValoresRepositoryReadOnly>();
            container.RegisterType<IDespesasRepositoryReadOnly, DespesasRepositoryReadOnly>();
            container.RegisterType<IReceitasRepositoryReadOnly, ReceitasRepositoryReadOnly>();
            container.RegisterType<IRelatorioFinanceiroRepositoryReadOnly, RelatorioFinanceiroRepositoryReadOnly>();

            //DataConfig
            container.RegisterType(typeof (IContextManager<>), typeof (ContextManager<>));
            container.RegisterType<IDbContext, BarraFisikContext>();
            container.RegisterType(typeof (IUnitOfWork<>), typeof (UnitOfWork<>));

            return container;
        }
    }
}