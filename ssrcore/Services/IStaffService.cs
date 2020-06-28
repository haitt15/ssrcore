using ssrcore.Helpers;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IStaffService
    {
        Task<PagedList<UserModel>> GetAllStaffs(SearchStaffModel model);
        Task<UserModel> GetStaff(int staffId);
        Task<UserModel> CreateStaff(UserModel staff);
        Task<bool> DeleteStaff(int staffId);
        Task<UserModel> UpdateStaff(int staffId, UserModel staff);
    }
}
