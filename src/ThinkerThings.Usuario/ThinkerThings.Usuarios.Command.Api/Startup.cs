using Microsoft.Owin;
using Owin;
using SimpleInjector;
using System.Web.Http;
using ThinkerThings.Usuarios.Command.Api.Helpers;

[assembly: OwinStartup(typeof(ThinkerThings.Usuarios.Command.Api.Startup))]

namespace ThinkerThings.Usuarios.Command.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var container = new Container();

            container.Initialize(app, config);
            container.ConfigureEventBus();

            config.ConfigureWebApi();
            config.ConfigureRoutes();

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureEventBus(Container container)
        {

        }
    }
}
