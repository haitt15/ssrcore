using AutoMapper;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.AutoMapper
{
    public class StaffMapper : Profile
    {
        public StaffMapper()
        {
            CreateMap<UserModel, Staff>()
                .ForMember(
                    dest => dest.StaffId,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}
