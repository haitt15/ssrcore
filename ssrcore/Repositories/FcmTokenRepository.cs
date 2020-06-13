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

        public async Task Create(int UserId, string FcmToken)
        {
            var token = _context.FcmToken.SingleOrDefault(s => s.FcmToken1 == FcmToken);
            if (token == null)
            {
                try
                {
                    var fcmToken = new FcmToken
                    {
                        UserId = UserId,
                        FcmToken1 = FcmToken
                    };
                    await _context.AddAsync(fcmToken);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<string> GetFcmToken(int UserId)
        {
            var query = _context.FcmToken.Where(s => s.UserId == UserId).Select(s => s.FcmToken1).ToList();
            return query;
        }
    }
}
