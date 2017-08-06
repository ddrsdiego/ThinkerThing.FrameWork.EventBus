using EventBus.Abstractions;
using SimpleInjector;
using ThinkerThings.Dominio.Usuarios.Events;
using ThinkerThings.Usuarios.Event;

namespace ThinkerThings.Usuarios.Command.Api.Helpers
{
    public static class ConfigureEventBusEx
    {
        public static void ConfigureEventBus(this Container container)
        {
            var eventBus = container.GetInstance<IEventBus>();

            eventBus.Subscribe<UsuarioCriadoEvent>();
            eventBus.Subscribe<UsuarioAtualizadoEvent>();
        }
    }
}