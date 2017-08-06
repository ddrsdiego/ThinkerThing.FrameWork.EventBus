using Microsoft.Owin;
using Owin;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Web.Http;
using ThinkerThings.Usuarios.Command.Api.Helper;

[assembly: OwinStartup(typeof(ThinkerThings.Usuarios.Command.Api.Startup))]

namespace ThinkerThings.Usuarios.Command.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new Container();

            container.Initialize(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                                        name: "DefaultApi",
                                        routeTemplate: "api/{version}/{controller}/{id}",
                                        defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(GlobalConfiguration.Configuration);

            app.Use(async (context, next) =>
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next();
                }
            });
        }
    }
}
