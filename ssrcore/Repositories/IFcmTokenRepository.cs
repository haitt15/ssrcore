using ssrcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IFcmTokenRepository
    {
        Task<List<string>> Get(int userId);
        Task Create(int userId, string fcmtoken);
    }
}
