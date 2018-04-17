using System;
using System.Runtime.Caching;

namespace OK.Confix.Managers
{
    public class CachedDataManager : DefaultDataManager, IDataManager
    {
        private ConfixContext _context;
        private MemoryCache _cacher = MemoryCache.Default;
        private CacheItemPolicy _cacheItemPolicy;
        private string _cacheKeyPrefix = "OK.Confix.Cache";

        public CachedDataManager(ConfixContext context) : base(context)
        {
            _context = context;
            _cacheItemPolicy = new CacheItemPolicy() { SlidingExpiration = TimeSpan.FromSeconds(_context.Settings.CacheInterval.Value) };
        }

        public override T Get<T>(string key)
        {
            string cachekey = _cacheKeyPrefix + "." + key;

            if (_cacher.Contains(cachekey))
            {
                return (T)Convert.ChangeType(_cacher.Get(cachekey), typeof(T));
            }

            T value = base.Get<T>(key);

            _cacher.Add(new CacheItem(cachekey, value), _cacheItemPolicy);
            
            return value;
        }

        public override void Set<T>(string key, T value)
        {
            string cachekey = _cacheKeyPrefix + "." + key;

            base.Set(key, value);

            _cacher.Remove(cachekey);
            _cacher.Add(new CacheItem(cachekey, value), _cacheItemPolicy);
        }
    }
}