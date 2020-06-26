using ssrcore.Models;
using ssrcore.ViewModels;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IRoleRepository
    {
        Task Create(Role role);
        Role GetRole(Users user);
    }
}
