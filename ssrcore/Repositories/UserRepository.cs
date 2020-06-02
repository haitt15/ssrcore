using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using ssrcore.Helpers;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace ssrcore.Repositories
{
    public class UserRepository
    {
        //public UserRepository(ApplicationContext context) : base(context)
        //{

        //}

        //public async Task<Users> Create(Users user, string password)
        //{
        //    if (string.IsNullOrWhiteSpace(password))
        //    {
        //        throw new AppException("Password is required");
        //    }


        //    if (_context.Users.Any(x => x.Username == user.Username))
        //    {
        //        throw new AppException("Username \"" + user.Username + "\" is already taken");
        //    }

        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;

        //    user.InsBy = "Admin";
        //    user.InsDatetime = DateTime.Now;
        //    user.UpdBy = "Admin";
        //    user.UpdDatetime = DateTime.Now;

        //    await _context.Users.AddAsync(user);

        //    return user;
        //}


        //public bool Save()
        //{
        //    return _context.SaveChanges() > 0;
        //}

        //private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
        //    if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
        //    if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        for (int i = 0; i < computedHash.Length; i++)
        //        {
        //            if (computedHash[i] != storedHash[i]) return false;
        //        }
        //    }

        //    return true;
        //}

        //public bool CheckPassword(string username, string password)
        //{
        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //        return false;

        //    var user = _context.Users.SingleOrDefault(x => x.Username == username);

        //    if (user != null)
        //    {
        //        if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        //public Users FindByUsername(string username)
        //{
        //    return _context.Users.Find(username);
        //}

        //public async Task<Users> FindByEmail(string email)
        //{
        //    return await _context.Users.FindAsync(email);
        //}

        //public async Task<bool> AddUserLogin(Users user, UserLoginInfo info)
        //{
        //    var userLogin = new UserLogin{
        //        LoginProvider = info.Provider,
        //        ProviderKey = info.Key,
        //        ProviderDisplayName = info.ProviderDisplayName,
        //        Username = user.Username
        //    };

        //    await _context.UserLogin.AddAsync(userLogin);
        //    return true;
        //}

        //public async Task<Users> FindByLogin(string provider, string key)
        //{
        //    var user = await _context.UserLogin.Where(x => (x.LoginProvider == provider) && (x.ProviderKey == key))
        //                                       .Select(s => new Users
        //                                       {
        //                                           Username = s.Username
        //                                       }).FirstOrDefaultAsync();
        //        return user;
        //}
    }
}
