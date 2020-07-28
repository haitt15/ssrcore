using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllByUserId(int userId);
        Task<NotificationModel> GetByIdToModel(int id);
        Task<Notification> GetByIdToEntity(int id);
        Task Create(Notification noti);
        void Delete(Notification noti);
    }
}
