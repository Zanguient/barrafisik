using Microsoft.Practices.Unity;
using System.Web.Http;
using BarraFisik.API.helpers;
using BarraFisik.CrossCutting.IoC.Unity;
using Unity.WebApi;

namespace BarraFisik.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			//var container = new UnityContainer();           
   //         DependencyResolver.Resolve(container);
   //         config.DependencyResolver = new UnityResolver(container);

            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}