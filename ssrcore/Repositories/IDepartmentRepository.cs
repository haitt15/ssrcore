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
        Task<PagedList<DepartmentModel>> Get(SearchDepartmentModel model);
        Task<DepartmentModel> GetByIdToModel(string departmentId);
        Task<Department> GetByIdToEntity(string departmentId);
        Task Create(Department department);
        void Update(Department department);
        void Delete(Department department);
    }
}
