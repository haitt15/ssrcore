using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IDepartmentService
    {
        Task<object> GetAllDepartment(SearchDepartmentModel model);
        Task<DepartmentModel> GetDepartment(string departmentId);
        Task<DepartmentModel> CreateDepartment(DepartmentModel department);
        Task<bool> DeleteDepartment(string departmentId);
        Task<DepartmentModel> UpdateDepartment(string departmentId, DepartmentModel department);
        
    }
}
