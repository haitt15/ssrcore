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

        public async Task<IEnumerable<RequestHistoryModel>> GetAllRequestHistory(string ticketId)
        {
            var entities = await _unitOfWork.RequestHistoryRepository.GetAll(ticketId);
            return _mapper.Map<IEnumerable<RequestHistoryModel>>(entities);
        }

    }
}
