using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IRedisCacheRepository
    {
        void Add(string key, object value);
        Object Get(string key);
        T Get<T>(string key);
        void Delete(string key);
        bool isExist(string key);
    }
}
