using System;
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

namespace BarraFisik.CrossCutting.IoC.Unity
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
            UnityContainer container = new UnityContainer();

            //APP            
            container.RegisterType<IClienteAppService, ClienteAppService>();
            container.RegisterType<IHorarioAppService, HorarioAppService>();
            container.RegisterType<IFilaEsperaAppService, FilaEsperaAppService>();
            container.RegisterType<IMensalidadesAppService, MensalidadesAppService>();
            container.RegisterType<IValoresAppService, ValoresAppService>();

            //Services
            container.RegisterType(typeof(IServiceBase<>), typeof(ServiceBase<>));
            container.RegisterType<IClienteService, ClienteService>();
            container.RegisterType<IHorarioService, HorarioService>();
            container.RegisterType<IFilaEsperaService, FilaEsperaService>();
            container.RegisterType<IMensalidadesService, MensalidadesService>();
            container.RegisterType<IValoresService, ValoresService>();

            //Data Repos
            container.RegisterType(typeof(IRepositoryBase<>), typeof(RepositoryBase<,>));
            container.RegisterType<IClienteRepository, ClienteRepository>();
            container.RegisterType<IHorarioRepository, HorarioRepository>();
            container.RegisterType<IFilaEsperaRepository, FilaEsperaRepository>();
            container.RegisterType<IMensalidadesRepository, MensalidadesRepository>();
            container.RegisterType<IValoresRepository,ValoresRepository>();

            //Data Repos Read Only
            container.RegisterType<IClienteRepositoryReadOnly, ClienteRepositoryReadOnly>();
            container.RegisterType<IHorarioRepositoryReadOnly, HorarioRepositoryReadOnly>();
            container.RegisterType<IMensalidadesRepositoryReadOnly, MensalidadesRepositoryReadOnly>();
            container.RegisterType<IValoresRepositoryReadOnly, ValoresRepositoryReadOnly>();

            //DataConfig
            container.RegisterType(typeof(IContextManager<>), typeof(ContextManager<>));
            container.RegisterType<IDbContext, BarraFisikContext>();
            container.RegisterType(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return container;
        }
    }
}