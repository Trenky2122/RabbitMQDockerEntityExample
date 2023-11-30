using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQDockerEntityExample.Core.Storage
{
    public class Storage<TKey, TValue> : IStorage<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue?> _storage = new();

        public bool TryGetValue(TKey key, out TValue? value) => _storage.TryGetValue(key, out value);

        public TValue? SetValue(TKey key, TValue? value) => _storage[key] = value;

    }
}
