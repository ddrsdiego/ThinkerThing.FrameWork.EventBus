using Rydo.Framework.MediatR.IoC;
using SimpleInjector;

namespace ThinkerThings.Usuarios.IoC
{
    public static class ContainerEx
    {
        public static void RegistrarContainers(this Container container)
        {
            RegistrarRydoFrameworkMediatR.Registrar(container);

            HandlerContainer.Registrar(container);
            ServicoContainer.Registrar(container);
            RepositorioContainer.Registrar(container);
            EventBusContainer.Registrar(container);
        }
    }
}
