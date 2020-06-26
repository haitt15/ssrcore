using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IFcmTokenService
    {
        Task CreateFcmToken(int userId, string fcmToken);
        Task<List<string>> GetAllFcmToken(int userId);
    }
}
