using AutoMapper;
using ssrcore.Models;
using ssrcore.ViewModels;

namespace ssrcore.AutoMapper
{
    public class NotificationMapper : Profile
    {
        public NotificationMapper()
        {
            CreateMap<Notification, NotificationModel>();
            CreateMap<NotificationModel, Notification>();
        }
    }
}
