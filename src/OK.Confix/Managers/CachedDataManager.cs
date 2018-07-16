using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OK.Confix.Managers
{
    public class CacheItem<T>
    {
        public T Value { get; set; }

        public DateTime ExpirationDate { get; set; }

        public CacheItem(T value, int expiresInMs)
        {
            Value = value;
            ExpirationDate = DateTime.Now.AddMilliseconds(expiresInMs);
        }
    }

    public class CacheItem : CacheItem<object>
    {
        public CacheItem(object value, int expiresInMs) : base(value, expiresInMs)
        {

        }
    }

    internal class CachedDataManager : DefaultDataManager, IDataManager
    {
        private readonly ConfixContextSettings _settings;
        private readonly IDictionary<string, object> _locks;
        private readonly IDictionary<string, CacheItem> _values;

        public CachedDataManager(ConfixContextSettings settings) : base(settings)
        {
            _settings = settings;
            _locks = new Dictionary<string, object>();
            _values = new ConcurrentDictionary<string, CacheItem>();
        }

        public override T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            string cacheKey = _settings.Application.Name + "." + _settings.Environment?.Name + "." + key;

            if (!_locks.ContainsKey(cacheKey))
            {
                return default(T);
            }

            lock (_locks[cacheKey])
            {
                if (!_values.TryGetValue(cacheKey, out CacheItem item))
                {
                    return default(T);
                }

                if (item.ExpirationDate < DateTime.Now)
                {
                    T value = base.Get<T>(key);

                    _values[cacheKey] = new CacheItem(value, _settings.CacheInterval.Value);

                    return value;
                }

                return (T)Convert.ChangeType(item.Value, typeof(T));
            }
        }

        public override void Set<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            string cacheKey = _settings.Application.Name + "." + _settings.Environment?.Name + "." + key;

            if (!_locks.ContainsKey(cacheKey))
            {
                _locks.Add(cacheKey, new { });
            }

            lock (_locks[cacheKey])
            {
                base.Set(key, value);

                if (_values.ContainsKey(cacheKey))
                {
                    _values[cacheKey] = new CacheItem(value, _settings.CacheInterval.Value);
                }
                else
                {
                    _values.Add(cacheKey, new CacheItem(value, _settings.CacheInterval.Value));
                }
            }
        }
    }
}