using ssrcore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> GetByUsername(string username);
        Task<Users> GetByUserId(int? userId);
        Task<Users> Create(Users user, string password);
        void Delete(Users user);
    }
}
