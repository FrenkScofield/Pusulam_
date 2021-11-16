using System.Web.Http;
using WebActivatorEx;
using PusulamAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace PusulamAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
             .EnableSwagger(c =>
             {
                 c.SingleApiVersion("v1", "PusulamAPI");
                 c.IncludeXmlComments(string.Format(@"{0}\bin\PusulamAPI.XML", System.AppDomain.CurrentDomain.BaseDirectory));
             }).EnableSwaggerUi(c => c.DisableValidator());
        }
    }
}
