using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Infrastructure.Interfaces;

namespace UstaPlatform.Infrastructure.Repositories
{
    /// <summary>
    /// Bellek içi repository implementasyonu (demo amaçlı)
    /// </summary>
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly Dictionary<string, T> _storage = new Dictionary<string, T>();
        private readonly Func<T, string> _getKey;

        public InMemoryRepository(Func<T, string> getKey)
        {
            _getKey = getKey ?? throw new ArgumentNullException("getKey");
        }

        public T GetById(string id)
        {
            T value;
            if (_storage.TryGetValue(id, out value))
                return value;
            return null;
        }

        public IEnumerable<T> GetAll()
        {
            return _storage.Values.ToList();
        }

        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var key = _getKey(entity);
            _storage[key] = entity;
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var key = _getKey(entity);
            if (!_storage.ContainsKey(key))
                throw new InvalidOperationException("Entity with key '" + key + "' not found.");
            _storage[key] = entity;
        }

        public void Delete(string id)
        {
            _storage.Remove(id);
        }

        public int Count
        {
            get { return _storage.Count; }
        }
    }
}
