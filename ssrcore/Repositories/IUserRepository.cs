using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IUserRepository
    {
        Task<Users> Create(Users user, string password);
        Task<bool> CheckPassword(string username, string password);
        Task<Users> FindByUsername(string username);
        Task<Users> FindByEmail(string email);
        //Task<bool> AddUserLogin(Users user, UserLoginInfo info);
        //Task<Users> FindByLogin(string provider, string key);
        Task<bool> Save();

    }
}
