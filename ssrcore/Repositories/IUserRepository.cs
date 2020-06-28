using ssrcore.Models;
using ssrcore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> GetByUsername(string username);
        Task<Users> Create(Users user, string password);
        void Update(Users user);
        void Delete(Users user);
    }
}
