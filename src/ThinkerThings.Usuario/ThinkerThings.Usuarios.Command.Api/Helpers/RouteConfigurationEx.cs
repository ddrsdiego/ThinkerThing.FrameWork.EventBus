using System.Web.Http;

namespace ThinkerThings.Usuarios.Command.Api.Helpers
{
    public static class RouteConfigurationEx
    {
        public static void ConfigureRoutes(this HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}