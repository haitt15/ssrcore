using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetByUserName(string username);
        Task<Users> CreateUser(Users user, string password);
        Task<bool> CheckPassWord(string username, string password);
        Task<UserModel> UpdateUser(string username, UserModel user);
        Task<bool> DeleteUser(string username);
    }
}
