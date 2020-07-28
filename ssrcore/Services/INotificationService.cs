using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationModel>> GetAllNotificationByUsername(string username);
        Task<NotificationModel> GetNotificationById(int id);
    }
}
