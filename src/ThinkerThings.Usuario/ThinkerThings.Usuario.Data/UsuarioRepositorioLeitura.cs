using StackExchange.Redis;
using System;
using System.Collections.Generic;
using ThinkerThings.Dominio.Usuarios.Contratos.Repositorios;
using ThinkerThings.Dominio.Usuarios.Model;

namespace ThinkerThings.Usuarios.Data
{
    public class UsuarioRepositorioLeitura : RepositorioBase, IUsuarioRepositorioLeitura
    {
        private readonly string _nameSpace;
        private readonly IConnectionMultiplexer _redisConnection;

        public UsuarioRepositorioLeitura(IConnectionMultiplexer redisConnection, string nameSpace) 
            : base(redisConnection, nameSpace)
        {
            _nameSpace = nameSpace;
            _redisConnection = redisConnection;
        }

        public IEnumerable<Usuario> Consultar(List<int> ids)
        {
            return GetMultiple<Usuario>(ids);
        }

        public Usuario Consultar(int id)
        {
            return Get<Usuario>(id);
        }

        public bool Existe(int id)
        {
            throw new NotImplementedException();
        }

        public void Salvar(Usuario item)
        {
            throw new NotImplementedException();
        }
    }
}
