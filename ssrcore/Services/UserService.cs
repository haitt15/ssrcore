using AutoMapper;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.UnitOfWork;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CheckPassWord(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = await _unitOfWork.UserRepository.GetByUsername(username);

            if (user != null)
            {
                if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<Users> CreateUser(Users user, string password)
        {
            await _unitOfWork.UserRepository.Create(user, password);
            await _unitOfWork.Commit();
            return user;
        }

        public async Task<bool> DeleteUser(string username)
        {
            var entity = await _unitOfWork.UserRepository.GetByUsername(username);
            if (entity != null)
            {
                _unitOfWork.UserRepository.Delete(entity);
                await _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            return users;
        }

        public async Task<Users> GetByUserName(string username)
        {
            var entity = await _unitOfWork.UserRepository.GetByUsername(username);
            if (entity == null)
            {
                throw new AppException("Cannot find " + username);
            }
            return entity;
        }

        public async Task<UserModel> UpdateUser(string username, UserModel user)
        {
            var entity = await _unitOfWork.UserRepository.GetByUsername(username);
            if (entity != null)
            {
                entity.FullName = user.FullName != null ? user.FullName : entity.FullName;
                entity.Phonenumber = user.Phonenumber != null ? user.Phonenumber : entity.Phonenumber;
                entity.Photo = user.Photo != null ? user.Photo : entity.Photo;
                entity.Email = user.Email != null ? user.Email : entity.Email;
                entity.DelFlg = true;
                entity.UpdDatetime = DateTime.Now;
                return _mapper.Map<UserModel>(entity);
            }
            return null;
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
