using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IDepartmentRepository
    {
        Task<PagedList<DepartmentModel>> GetAllDepartments(SearchDepartmentModel model);
        Task<Department> GetDepartment(string departmentId);
        Task<bool> Create(DepartmentModel model);
        void UpdateDepartment(DepartmentModel model);
        Task<bool> Remove(string departmentId);
    }
}
