using AutoMapper;
using ssrcore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public class FcmTokenService : IFcmTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FcmTokenService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateFcmToken(int userId, string fcmToken)
        {
            await _unitOfWork.FcmTokenRepository.Create(userId, fcmToken);
            await _unitOfWork.Commit();
        }

        public Task<List<string>> GetAllFcmToken(int userId)
        {
            var result = _unitOfWork.FcmTokenRepository.Get(userId);
            return result;
        }
    }
}
