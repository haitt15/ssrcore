using AutoMapper;
using ssrcore.Helpers;
using ssrcore.UnitOfWork;
using ssrcore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationModel>> GetAllNotificationByUsername(string username)
        {
            var user = await _unitOfWork.UserRepository.GetByUsername(username);
            var result = await _unitOfWork.NotificationRepository.GetAllByUserId(user.Id);
            return _mapper.Map<IEnumerable<NotificationModel>>(result);
        }

        public async Task<NotificationModel> GetNotificationById(int id)
        {
            var noti = await _unitOfWork.NotificationRepository.GetByIdToModel(id);
            if (noti == null)
            {
                throw new AppException("Cannot find " + id);
            }
            return noti;
        }

    }
}
