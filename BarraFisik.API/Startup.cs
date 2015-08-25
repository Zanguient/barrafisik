using System.Linq;
using System.Web.Http;
using BarraFisik.Application.AutoMapper;
using Microsoft.Owin.Cors;
using Owin;

namespace BarraFisik.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfig = new HttpConfiguration();

            ConfigureWebApi(httpConfig);

            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}