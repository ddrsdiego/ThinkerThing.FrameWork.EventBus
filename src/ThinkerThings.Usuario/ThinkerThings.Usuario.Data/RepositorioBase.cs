using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace ThinkerThings.Usuarios.Data
{
    public class RepositorioBase
    {
        private readonly string _nameSpace;
        private readonly IConnectionMultiplexer _redisConnection;

        public RepositorioBase(IConnectionMultiplexer redisConnection, string nameSpace)
        {
            _nameSpace = nameSpace;
            _redisConnection = redisConnection;
        }

        #region Métodos Privados

        private string MakeKey(string keySuffix)
        {
            if (!keySuffix.StartsWith(_nameSpace + ":"))
            {
                return _nameSpace + ":" + keySuffix;
            }
            else return keySuffix; //Key is already suffixed with namespace
        }

        private string MakeKey(int id)
        {
            return MakeKey(id.ToString());
        }

        private T Get<T>(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            if (serializedObject.IsNullOrEmpty) throw new ArgumentNullException(); //Throw a better exception than this, please
            return JsonConvert.DeserializeObject<T>(serializedObject.ToString());
        }

        private bool Exists(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            return !serializedObject.IsNullOrEmpty;
        }

        private void Save(string keySuffix, object entity)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            database.StringSet(MakeKey(key), JsonConvert.SerializeObject(entity));
        }

        #endregion Métodos Privados

        #region Métodos Publicos

        public List<T> GetMultiple<T>(List<int> ids)
        {
            var items = new List<T>();
            var keys = new List<RedisKey>();
            ids.ForEach(id => { keys.Add(MakeKey(id)); });

            var dataBase = _redisConnection.GetDatabase();
            var serializedItems = dataBase.StringGet(keys.ToArray(), CommandFlags.None);

            foreach (var item in serializedItems)
                items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));

            return items;
        }

        public T Get<T>(int id)
        {
            return Get<T>(id.ToString());
        }

        public bool Exists(int id)
        {
            return Exists(id.ToString());
        }

        public void Save(int id, object entity)
        {
            Save(id.ToString(), entity);
        }

        #endregion Métodos Publicos
    }
}
