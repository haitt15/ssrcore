﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace ssrcore.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Users> Create(Users user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }


            if (_context.Users.Any(x => x.Username == user.Username))
            {
                throw new AppException("Username \"" + user.Username + "\" is already taken");
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.InsBy = "Admin";
            user.InsDatetime = DateTime.Now;
            user.UpdBy = "Admin";
            user.UpdDatetime = DateTime.Now;

            await _context.Users.AddAsync(user);
            return user;

        }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<Users> GetByUsername(string username)
        {
            var user = await _context.Users.Where(x => x.Username == username)
                                       .SingleOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public void Delete(Users user)
        {
            user.DelFlg = true;
        }

        public async Task<Users> GetByUserId(int? userId)
        {
            return await _context.Users.Where(x => x.Id == userId)
                                       .SingleOrDefaultAsync();
        }

    }
}
