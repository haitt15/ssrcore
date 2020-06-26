using ssrcore.Helpers;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IUserService
    {
        Task<object> GetAllUsers(SearchUserModel model);
        Task<UserModel> GetByUserName(string username);
        Task<UserModel> GetByUserId(string uid);
        Task<UserModel> CreateUser(RegisterModel model);
        Task<bool> CheckPassWord(string username, string password);
        Task<UserModel> UpdateUser(string uid, UserModel user);
        Task<bool> DeleteUser(string uid);
    }
}
