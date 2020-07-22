using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public class RedisCacheRepository : IRedisCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public void Add(string key, object value)
        {
            _distributedCache.SetString(key, JsonConvert.SerializeObject(value));
        }

        public void Delete(string key)
        {
            _distributedCache.Remove(key);
        }

        public object Get(string key)
        {
           return JsonConvert.DeserializeObject(_distributedCache.GetString(key));
        }

        public T Get<T>(string key)
        {
            var result = _distributedCache.GetString(key);
             if(result == null)
            {
                return default(T);
            }
            return (T)JsonConvert.DeserializeObject(result, typeof(T));
        }

        public bool isExist(string key)
        {
            return _distributedCache.GetString(key) != null;
        }

    }
}
