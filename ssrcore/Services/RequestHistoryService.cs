using AutoMapper;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.UnitOfWork;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public class RequestHistoryService : IRequestHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RequestHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RequestHistoryModel> CreateRequestHistory(RequestHistoryModel model)
        {
            var entity = _mapper.Map<RequestHistory>(model);
            _unitOfWork.RequestHistoryRepository.Create(entity);
            await _unitOfWork.Commit();
            return _mapper.Map<RequestHistoryModel>(entity);
        }

        public async Task<IEnumerable<RequestHistoryModel>> GetAllRequestHistory()
        {
            var entities = await _unitOfWork.RequestHistoryRepository.GetAll();
            return _mapper.Map<IEnumerable<RequestHistoryModel>>(entities);
        }

        public async Task<RequestHistoryModel> GetRequestHistory(string ticketId)
        {
            var entity = await _unitOfWork.RequestHistoryRepository.GetById(ticketId);
            if(entity == null)
            {
                throw new AppException("Cannot find " + ticketId);
            }
            return _mapper.Map<RequestHistoryModel>(entity);
        }
    }
}
