using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;
using ThinkerThings.Usuarios.IoC;

namespace ThinkerThings.Usuarios.Command.Api.Helper
{
    public static class SimpleInjectorWebApiInitializer
    {
        public static void Initialize(this Container container, HttpConfiguration configuration)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.RegistrarContainers();
            container.RegisterWebApiControllers(configuration);
            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}