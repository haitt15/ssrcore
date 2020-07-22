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
            if (user != null)
            {
                var service = await _unitOfWork.ServiceRepository.GetByIdToModel(serviceRequest.ServiceId);
                serviceRequest.UserId = user.Id;
                serviceRequest.Status = "Waiting";
                serviceRequest.DueDateTime = DateTime.Now.AddDays(service.ProcessMaxDay);
                var entity = _mapper.Map<ServiceRequest>(serviceRequest);
                await _unitOfWork.ServiceRequestRepository.Create(entity);
                await _unitOfWork.Commit();
                var modelToReturn = await _unitOfWork.ServiceRequestRepository.GetByIdToModel(entity.TicketId);
                return modelToReturn;
            }
            return null;
        }

        public async Task<bool> DeleteServiceRequest(string ticketId)
        {
            var entity = await _unitOfWork.ServiceRequestRepository.GetByIdToEntity(ticketId);
            if (entity != null)
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
                            case "StudentPhoto":
                                dictionary.Add("Photo", s.StudentPhoto);
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
                            case "DepartmentId":
                                dictionary.Add("Department", s.DepartmentId);
                                break;
                            case "DepartmentNm":
                                dictionary.Add("Department", s.DepartmentNm);
                                break;
                            case "StaffId":
                                dictionary.Add("StaffId", s.StaffId);
                                break;
                            case "StaffNm":
                                dictionary.Add("StaffNm", s.StaffNm);
                                break;
                            case "StaffUsername":
                                dictionary.Add("StaffUsername", s.StaffUsername);
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
            var serviceRequest = await _unitOfWork.ServiceRequestRepository.GetByIdToModel(ticketId);
            if (serviceRequest == null)
            {
                throw new AppException("Cannot find " + ticketId);
            }
            return serviceRequest;
        }


        public async Task<IEnumerable<ServiceRequestModel>> GetServiceRequestByUserId(int userId)
        {
            var requests = await _unitOfWork.ServiceRequestRepository.GetByUserId(userId);
            if (requests == null)
            {
                throw new AppException("Cannot find " + userId);
            }
            return _mapper.Map<IEnumerable<ServiceRequestModel>>(requests);
        }

        public async Task<ServiceRequestModel> UpdateServiceRequest(string ticketId, ServiceRequestModel serviceRequest)
        {
            if(serviceRequest.StaffUsername != null)
            {
                var staff = await _unitOfWork.UserRepository.GetByUsername(serviceRequest.StaffUsername);
                serviceRequest.StaffId = staff.Id;
            }
            var entity = await _unitOfWork.ServiceRequestRepository.GetByIdToEntity(ticketId);
            entity.ServiceId = serviceRequest.ServiceId != null ? serviceRequest.ServiceId : entity.ServiceId;
            entity.StaffId = serviceRequest.StaffId != null ? serviceRequest.StaffId : entity.StaffId;
            entity.Content = serviceRequest.Content != null ? serviceRequest.Content : entity.Content;
            entity.JsonInformation = serviceRequest.JsonInformation != null ? serviceRequest.JsonInformation : entity.JsonInformation;
            entity.DueDateTime = serviceRequest.DueDateTime.Year >= 1753 ? serviceRequest.DueDateTime : entity.DueDateTime;
            entity.Status = serviceRequest.Status != null ? serviceRequest.Status : entity.Status;
            entity.UpdBy = serviceRequest.implementer != null ? serviceRequest.implementer : entity.UpdBy;
            entity.UpdDatetime = DateTime.Now;
            await _unitOfWork.Commit();
            var modelToReturn = await _unitOfWork.ServiceRequestRepository.GetByIdToModel(ticketId);
            return modelToReturn;
        }

        public async Task UpdateStatusExpiredServiceRequest()
        {
            var list_serviceRequest = await _unitOfWork.ServiceRequestRepository.GetExpiredRequest();
            foreach (var item in list_serviceRequest)
            {
                if (item.DueDateTime < DateTime.Now)
                {
                    item.Status = "Expired";
                }
                if(item.DueDateTime > DateTime.Now && item.Status == "Expired")
                {
                    item.Status = "In Progress";
                }
            }

            await _unitOfWork.Commit();
        }
    }
}
