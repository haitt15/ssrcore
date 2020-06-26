using Microsoft.EntityFrameworkCore;
using ssrcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public class FcmTokenRepository : BaseRepository, IFcmTokenRepository
    {
        public FcmTokenRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task Create(int userId, string fcmtoken)
        {
            var token = await _context.FcmToken.SingleOrDefaultAsync(s => s.FcmToken1 == fcmtoken);
            if (token == null)
            {
                var fcmToken = new FcmToken
                {
                    UserId = userId,
                    FcmToken1 = fcmtoken
                };
                await _context.AddAsync(fcmToken);
            }
        }

        public async Task<List<string>> Get(int userId)
        {
            var query = await _context.FcmToken.Where(s => s.UserId == userId)
                                               .Select(s => s.FcmToken1).ToListAsync();
            return query;
        }
    }
}
