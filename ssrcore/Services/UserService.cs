using AutoMapper;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.UnitOfWork;
using ssrcore.ViewModels;
using System;
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

        public async Task<UserModel> CreateUser(RegisterModel model)
        {
            var entity = _mapper.Map<Users>(model);
            await _unitOfWork.UserRepository.Create(entity, model.Password);
            await _unitOfWork.Commit();
            return _mapper.Map<UserModel>(entity);
        }

        public async Task<bool> DeleteUser(string uid)
        {
            var entity = await _unitOfWork.UserRepository.GetByUid(uid);
            if(entity != null)
            {
                _unitOfWork.UserRepository.Delete(entity);
                await _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public Task<object> GetAllUsers(SearchUserModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetByUserId(string uid)
        {
            var entity = await _unitOfWork.UserRepository.GetByUid(uid);
            if(entity == null)
            {
                throw new AppException("Cannot find " + uid);
            }
            return _mapper.Map<UserModel>(entity);
        }

        public async Task<UserModel> GetByUserName(string username)
        {
            var entity = await _unitOfWork.UserRepository.GetByUsername(username);
            if (entity == null)
            {
                throw new AppException("Cannot find " + username);
            }
            return _mapper.Map<UserModel>(entity);
        }

        public Task<UserModel> UpdateUser(string uid, UserModel user)
        {
            throw new NotImplementedException();
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
