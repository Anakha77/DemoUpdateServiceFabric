using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Owin;

namespace Matrix.ServiceWeb
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            FormatterConfig.ConfigureFormatters(config.Formatters);
            config.MapHttpAttributeRoutes();

            appBuilder.UseWebApi(config);
        }
    }

    public static class FormatterConfig
    {
        public static void ConfigureFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.None;
        }
    }
}
