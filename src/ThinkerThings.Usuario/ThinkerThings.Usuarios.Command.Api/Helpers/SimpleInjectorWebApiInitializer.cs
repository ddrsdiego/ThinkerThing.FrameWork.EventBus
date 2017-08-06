using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;
using ThinkerThings.Usuarios.IoC;

namespace ThinkerThings.Usuarios.Command.Api.Helpers
{
    public static class SimpleInjectorWebApiInitializer
    {
        public static void Initialize(this Container container, IAppBuilder app, HttpConfiguration configuration)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.RegistrarContainers();
            container.RegisterWebApiControllers(configuration);

            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

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