using ssrcore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IUserRepository
    {
        Task<Users> Create(Users user, string password);
        Task<bool> CheckPassword(string username, string password);
        Task<Users> FindByUsername(string username);
        Task<Users> FindByEmail(string email);
        Task<IEnumerable<Users>> GetUsers();
        Task<bool> Save();

    }
}
