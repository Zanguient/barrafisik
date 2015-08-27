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
using Ninject.Modules;

namespace BarraFisik.Inra.CrossCutting.IoC
{
    public class NinjectModulo : NinjectModule
    {
        public override void Load()
        {

            //APP
            Bind<IClienteAppService>().To<ClienteAppService>();
            Bind<IHorarioAppService>().To<HorarioAppService>();

            //Services
            Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            Bind<IClienteService>().To<ClienteService>();
            Bind<IHorarioService>().To<HorarioService>();

            //Data Repos
            Bind(typeof (IRepositoryBase<>)).To(typeof (RepositoryBase<,>));
            Bind<IClienteRepository>().To<ClienteRepository>();
            Bind<IHorarioRepository>().To<HorarioRepository>();

            //Data Repos Read Only
            Bind<IClienteRepositoryReadOnly>().To<ClienteRepositoryReadOnly>();

            //DataConfig
            Bind(typeof(IContextManager<>)).To(typeof(ContextManager<>));
            Bind<IDbContext>().To<BarraFisikContext>();
            Bind(typeof(IUnitOfWork<>)).To(typeof(UnitOfWork<>));
        }
    }
}