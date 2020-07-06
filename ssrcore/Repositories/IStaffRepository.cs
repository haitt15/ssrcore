using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IStaffRepository
    {
        Task<PagedList<UserModel>> Get(SearchStaffModel model);
        Task<UserModel> GetByIdToModel(int staffId);
        Task<Staff> GetByIdToEntity(int staffId);
        Task Create(Staff staff);
        void Update(Staff staff);
        void Delete(Staff staff);
    }
}
