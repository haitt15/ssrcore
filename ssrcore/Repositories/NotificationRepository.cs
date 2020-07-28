using Microsoft.EntityFrameworkCore;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task Create(Notification noti)
        {
            noti.DelFlg = false;
            noti.InsDatetime = DateTime.Now;
            noti.UpdDatetime = DateTime.Now;
            await _context.Notification.AddAsync(noti);
        }

        public void Delete(Notification noti)
        {
            noti.DelFlg = true;
        }

        public async Task<IEnumerable<Notification>> GetAllByUserId(int userId)
        {
            return await _context.Notification.Where(t => t.DelFlg == false && t.UserId == userId).ToListAsync();
        }

        public async Task<Notification> GetByIdToEntity(int id)
        {
            return await _context.Notification.Where(t => t.Id == id && t.DelFlg == false)
                                              .FirstOrDefaultAsync();
        }

        public async Task<NotificationModel> GetByIdToModel(int id)
        {
            return await _context.Notification.Where(t => t.Id == id && t.DelFlg == false)
                                              .Select(s => new NotificationModel 
                                              { 
                                                Id = s.Id,
                                                Title = s.Title,
                                                Content = s.Content,
                                                UserId = s.UserId,
                                                IsRead = s.IsRead,
                                                DelFlg = s.DelFlg,
                                                InsBy = s.InsBy,
                                                InsDatetime = s.InsDatetime,
                                                UpdBy = s.UpdBy,
                                                UpdDatetime = s.UpdDatetime
                                              })
                                              .FirstOrDefaultAsync();
        }
    }
}
