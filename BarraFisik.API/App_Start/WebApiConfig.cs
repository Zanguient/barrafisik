using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BarraFisik.API.App_Start;
using BarraFisik.Inra.CrossCutting.IoC;

namespace BarraFisik.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(new Container().GetModule());
        }
    }
}
