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
using Microsoft.Practices.Unity;

namespace BarraFisik.CrossCutting.IoC.Unity
{
    public class DependencyResolver
    {

        private static IUnityContainer ConfigureUnityContainer()
        {
            UnityContainer container = new UnityContainer();
            
            //APP
            container.RegisterType<IClienteAppService, ClienteAppService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHorarioAppService, HorarioAppService>(new ContainerControlledLifetimeManager());

            //Services
            container.RegisterType(typeof(IServiceBase<>), typeof(ServiceBase<>));
            container.RegisterType<IClienteService, ClienteService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHorarioService, HorarioService>(new ContainerControlledLifetimeManager());

            //Data Repos
            container.RegisterType(typeof(IRepositoryBase<>), typeof(RepositoryBase<,>));
            container.RegisterType<IClienteRepository, ClienteRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHorarioRepository, HorarioRepository>(new ContainerControlledLifetimeManager());

            //Data Repos Read Only
            container.RegisterType<IClienteRepositoryReadOnly, ClienteRepositoryReadOnly>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHorarioRepositoryReadOnly, HorarioRepositoryReadOnly>(new ContainerControlledLifetimeManager());

            //DataConfig
            container.RegisterType(typeof(IContextManager<>), typeof(ContextManager<>));
            container.RegisterType<IDbContext, BarraFisikContext>(new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
           
            return container;
        }

        //public static void Resolve(UnityContainer container)
        //{
        //    //APP
        //    container.RegisterType<IClienteAppService, ClienteAppService>(new HierarchicalLifetimeManager());
        //    container.RegisterType<IHorarioAppService, HorarioAppService>(new HierarchicalLifetimeManager());

        //    //Services
        //    container.RegisterType(typeof(IServiceBase<>), typeof(ServiceBase<>));
        //    container.RegisterType<IClienteService, ClienteService>(new HierarchicalLifetimeManager());
        //    container.RegisterType<IHorarioService, HorarioService>(new HierarchicalLifetimeManager());

        //    //Data Repos
        //    container.RegisterType(typeof(IRepositoryBase<>), typeof(RepositoryBase<,>));
        //    container.RegisterType<IClienteRepository, ClienteRepository>(new HierarchicalLifetimeManager());
        //    container.RegisterType<IHorarioRepository, HorarioRepository>(new HierarchicalLifetimeManager());

        //    //Data Repos Read Only
        //    container.RegisterType<IClienteRepositoryReadOnly, ClienteRepositoryReadOnly>(new HierarchicalLifetimeManager());
        //    container.RegisterType<IHorarioRepositoryReadOnly, HorarioRepositoryReadOnly>(new HierarchicalLifetimeManager());

        //    //DataConfig
        //    container.RegisterType(typeof(IContextManager<>), typeof(ContextManager<>));
        //    container.RegisterType<IDbContext, BarraFisikContext>(new HierarchicalLifetimeManager());
        //    container.RegisterType(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        //}
    }
}