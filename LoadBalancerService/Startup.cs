using System.Web.Http;
using Matrix.Common;
using Owin;

namespace LoadBalancerService
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 256;
            var config = new HttpConfiguration();
            FormatterConfig.ConfigureFormatters(config.Formatters);
            config.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(config);
        }
    }
}
