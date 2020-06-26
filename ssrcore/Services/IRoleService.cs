using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IRoleService
    {
        Task<Role> CreateRole(RoleModel model);
        string GetRole(Users user);
    }
}
