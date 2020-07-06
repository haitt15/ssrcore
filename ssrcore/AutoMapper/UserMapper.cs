using AutoMapper;
using ssrcore.Models;
using ssrcore.ViewModels;

namespace ssrcore.AutoMapper
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterModel, Users>();
            CreateMap<UserModel, Users>();
            CreateMap<Users, UserModel>();
        }
    }
}
