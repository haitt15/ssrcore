using AutoMapper;
using ssrcore.Models;
using ssrcore.ViewModels;

namespace ssrcore.AutoMapper
{
    public class DepartmentMapper : Profile
    {
        public DepartmentMapper()
        {
            CreateMap<Department, DepartmentModel>();
            CreateMap<DepartmentModel, Department>();
        }
    }
}
