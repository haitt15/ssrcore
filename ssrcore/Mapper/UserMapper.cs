using AutoMapper;
using ssrcore.Models;
using ssrcore.ViewModels;

namespace ssrcore.Mapper
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterModel, Users>();
        }
    }
}
