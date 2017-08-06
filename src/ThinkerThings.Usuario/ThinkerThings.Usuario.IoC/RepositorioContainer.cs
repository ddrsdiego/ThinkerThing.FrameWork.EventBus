using SimpleInjector;
using ThinkerThings.Dominio.Usuarios.Contratos.Repositorios;
using ThinkerThings.Usuarios.Data;

namespace ThinkerThings.Usuarios.IoC
{
    public static class RepositorioContainer
    {
        public static void Registrar(Container container)
        {
            //container.Register<IUsuarioRepositorioLeitura, UsuarioRepositorioLeitura>(Lifestyle.Scoped);
            container.Register<IUsuarioRepositorioEscrita, UsuarioRepositorioEscrita>(Lifestyle.Scoped);
        }
    }
}
