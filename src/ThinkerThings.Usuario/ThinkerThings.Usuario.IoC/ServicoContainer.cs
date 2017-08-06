using SimpleInjector;
using ThinkerThings.Dominio.Usuarios.Contratos.Servicos;
using ThinkerThings.Dominio.Usuarios.Servico;

namespace ThinkerThings.Usuarios.IoC
{
    public static class ServicoContainer
    {
        public static void Registrar(Container container)
        {
            container.Register<IUsuarioServico, UsuarioServico>(Lifestyle.Scoped);
        }
    }
}
