using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace MFramework.Common.Core.Caches
{

    public sealed class MemoryCacheCollection : ICacheCollectionExtended
    {
        private static readonly object _lock = new object();

        public MemoryCacheCollection()
        {
            Cache = MemoryCache.Default;
        }

        public int Count
        {
            get
            {
                return Cache.Count();
            }
        }

        private MemoryCache Cache { get; set; }

        public object this[string key]
        {
            get
            {
                return Cache[key];
            }

            set
            {
                Cache[key] = value;
            }
        }

        public void Add(string key,
                        object value)
        {
            Cache.Add(key, value, null);
        }

        public void Add(string key,
                        object value,
                        DateTime absoluteExpiration)
        {
            Cache.Add(key, value, absoluteExpiration);
        }

        public void Add(string key,
                        object value,
                        TimeSpan slidingExpiration)
        {
            var policy = new CacheItemPolicy
            {
                SlidingExpiration = slidingExpiration
            };
            Cache.Add(key, value, policy);
        }

        public void Clear()
        {
            lock (_lock)
            {
                var items = Cache.GetValues(Cache.Select(x => x.Key));
                if (null == items)
                {
                    return;
                }

                foreach (var item in items)
                {
                    Cache.Remove(item.Key);
                }
            }
        }

        public bool ContainsKey(string key)
        {
            return Cache.Select(x => x.Key).Any(x => x == key);
        }

        public object Get(string key)
        {
            return Cache.Get(key);
        }

        public T Get<T>(string key)
        {
            return (T)Cache.Get(key);
        }

        public object Remove(string key)
        {
            return Cache.Remove(key);
        }

        public T Remove<T>(string key)
        {
            return (T)Cache.Remove(key);
        }

        public void Set(string key,
                        object value)
        {
            Cache.Set(key, value, null);
        }

        public void Set(string key,
                        object value,
                        DateTime absoluteExpiration)
        {
            Cache.Set(key, value, absoluteExpiration);
        }

        public void Set(string key,
                        object value,
                        TimeSpan slidingExpiration)
        {
            var policy = new CacheItemPolicy
            {
                SlidingExpiration = slidingExpiration
            };
            Cache.Set(key, value, policy);
        }

        public object Get(string key,
                          Func<object> add)
        {
            if (null == add)
            {
                throw new ArgumentNullException("add");
            }

            if (ContainsKey(key))
            {
                return Cache.Get(key);
            }

            var value = add.Invoke();
            Add(key, value);

            return value;
        }

        public object Get(string key,
                          Func<ICacheCollection, object> add)
        {
            if (null == add)
            {
                throw new ArgumentNullException("add");
            }

            return ContainsKey(key)
                ? Cache.Get(key)
                : add.Invoke(this);
        }

        public T Get<T>(string key,
                        Func<T> add)
        {
            if (null == add)
            {
                throw new ArgumentNullException("add");
            }

            if (ContainsKey(key))
            {
                return (T)Cache.Get(key);
            }

            var value = add.Invoke();
            Add(key, value);

            return value;
        }

        public T Get<T>(string key,
                        Func<ICacheCollection, T> add)
        {
            if (null == add)
            {
                throw new ArgumentNullException("add");
            }

            if (ContainsKey(key))
            {
                return (T)Cache.Get(key);
            }

            return add.Invoke(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<object> GetEnumerator()
        {
            var items = Cache.GetValues(Cache.Select(x => x.Key));
            if (null == items)
            {
                yield break;
            }

            foreach (var item in items)
            {
                yield return item.Value;
            }
        }
    }
}
