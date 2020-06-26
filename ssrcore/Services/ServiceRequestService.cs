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
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceRequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceRequestModel> CreateServiceRequest(ServiceRequestModel serviceRequest)
        {
            var user = await _unitOfWork.UserRepository.GetByUsername(serviceRequest.Username);
            serviceRequest.UserId = user.Id;
            var entity = _mapper.Map<ServiceRequest>(serviceRequest);
            await _unitOfWork.ServiceRequestRepository.Create(entity);
            await _unitOfWork.Commit();
            var modelToReturn = await _unitOfWork.ServiceRequestRepository.GetById(entity.TicketId);
            return modelToReturn;
        }

        public async Task<bool> DeleteServiceRequest(string ticketId)
        {
            var serviceRequest = await _unitOfWork.ServiceRequestRepository.GetById(ticketId);
            var entity = _mapper.Map<ServiceRequest>(serviceRequest);
            if (serviceRequest != null)
            {
                _unitOfWork.ServiceRequestRepository.Delete(entity);
                await _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public async Task<object> GetAllServiceRequest(SearchServiceRequestModel model)
        {
            var serviceRequests = await _unitOfWork.ServiceRequestRepository.GetAll(model);
            dynamic result;

            List<Dictionary<string, object>> listModel = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(model.Fields))
            {
                string[] filter = model.Fields.Split(",");
                foreach (var s in serviceRequests)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    for (int i = 0; i < filter.Length; i++)
                    {
                        switch (filter[i].Trim())
                        {
                            case "TicketId":
                                dictionary.Add("TicketId", s.TicketId);
                                break;
                            case "Username":
                                dictionary.Add("Username", s.Username);
                                break;
                            case "FullName":
                                dictionary.Add("FullName", s.FullName);
                                break;
                            case "UserId":
                                dictionary.Add("UserId", s.UserId);
                                break;
                            case "ServiceId":
                                dictionary.Add("ServiceId", s.ServiceId);
                                break;
                            case "ServiceNm":
                                dictionary.Add("ServiceNm", s.ServiceNm);
                                break;
                            case "StaffId":
                                dictionary.Add("StaffId", s.StaffId);
                                break;
                            case "StaffNm":
                                dictionary.Add("StaffNm", s.Staff);
                                break;
                            case "Content":
                                dictionary.Add("Content", s.Content);
                                break;
                            case "DueDateTime":
                                dictionary.Add("DueDateTime", s.DueDateTime);
                                break;
                            case "Status":
                                dictionary.Add("Status", s.Status);
                                break;
                        }
                    }
                    listModel.Add(dictionary);
                }
                result = listModel;
            }
            else
            {
                result = serviceRequests;
            }
            return new
            {
                data = result,
                totalCount = serviceRequests.TotalCount,
                totalPages = serviceRequests.TotalPages
            };
        }

        public async Task<ServiceRequestModel> GetServiceRequest(string ticketId)
        {
            var entity = await _unitOfWork.ServiceRequestRepository.GetById(ticketId);
            if (entity == null)
            {
                throw new AppException("Cannot find " + ticketId);
            }
            return entity;
        }

        public async Task<IEnumerable<ServiceRequestModel>> GetServiceRequestByUserId(int userId)
        {
            var requests = await _unitOfWork.ServiceRequestRepository.GetByUserId(userId);
            if(requests == null)
            {
                throw new AppException("Cannot find " + userId);
            }
            return _mapper.Map<IEnumerable<ServiceRequestModel>>(requests);
        }

        public async Task<ServiceRequestModel> UpdateServiceRequest(string ticketId, ServiceRequestModel serviceRequest)
        {
            var entity = await _unitOfWork.ServiceRequestRepository.GetById(ticketId);
            if(string.IsNullOrEmpty(serviceRequest.UserId.ToString()))
            {
                entity.UserId = serviceRequest.UserId;
            }
            entity.ServiceId = serviceRequest.ServiceId != null ? serviceRequest.ServiceId : entity.ServiceId;
            entity.StaffId = serviceRequest.StaffId != null ? serviceRequest.StaffId : entity.StaffId;
            entity.Content = serviceRequest.Content != null ? serviceRequest.Content : entity.Content;
            entity.DueDateTime = serviceRequest.DueDateTime != null ? serviceRequest.DueDateTime : entity.DueDateTime;
            entity.Status = serviceRequest.Status != null ? serviceRequest.Status : entity.Status;
            entity.DelFlg = false;
            entity.UpdDatetime = DateTime.Now;
            await _unitOfWork.Commit();
            var modelToReturn = await _unitOfWork.ServiceRequestRepository.GetById(ticketId);
            return modelToReturn;
        }
    }
}
