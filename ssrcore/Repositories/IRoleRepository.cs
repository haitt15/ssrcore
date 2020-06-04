using ssrcore.Models;
using ssrcore.ViewModels;
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
