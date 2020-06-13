using ssrcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IFcmTokenRepository
    {
        public List<string> GetFcmToken(int UserId);
        Task Create(int UserId, string FcmToken);
    }
}
