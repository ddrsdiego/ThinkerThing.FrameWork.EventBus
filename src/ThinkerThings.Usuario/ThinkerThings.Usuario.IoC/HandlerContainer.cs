using MediatR;
using Rydo.Framework.MediatR.Handlres;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ThinkerThings.Usuarios.IoC
{
    public static class HandlerContainer
    {
        public static void Registrar(Container container)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            assemblies.AddRange(GetAssemblies());

            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);
            container.Register(typeof(INotificationHandler<>), assemblies);

            container.Register(typeof(HandlerRequest<,>), assemblies);
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(INotification).GetTypeInfo().Assembly;
        }
    }
}
