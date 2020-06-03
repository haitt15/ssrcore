using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> CreateRole(RoleModel model);
        string FindRole(Users user);
        Task<bool> Save();
    }
}
