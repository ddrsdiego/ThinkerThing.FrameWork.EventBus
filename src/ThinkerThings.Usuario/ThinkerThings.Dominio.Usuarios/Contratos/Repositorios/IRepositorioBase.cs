using System.Collections.Generic;

namespace ThinkerThings.Dominio.Usuarios.Contratos.Repositorios
{
    public interface IRepositorioBase<T>
    {
        T Consultar(int id);
        IEnumerable<T> Consultar(List<int> ids);
        bool Existe(int id);
        void Salvar(T item);
    }
}
