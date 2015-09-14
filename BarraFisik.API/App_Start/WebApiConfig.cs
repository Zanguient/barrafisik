using System.Web.Http;
//using BarraFisik.Inra.CrossCutting.IoC;
using BarraFisik.CrossCutting.IoC.Unity;
using Unity.WebApi;

namespace BarraFisik.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );

            //var container = new UnityContainer();
            //DependencyResolver.Resolve(container);
            //config.DependencyResolver = new UnityResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(new Container().GetModule());

            //GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(new Container().GetModule());
        }
    }
}