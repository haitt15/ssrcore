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

                var history = new RequestHistory
                {
                    TicketId = entity.TicketId,
                    ContentHistory = "Insert by: " + user.FullName + " - Insert Datetime: " + DateTime.Now.ToLocalTime(),
                    UpdDatetime = DateTime.Now
                };
                _unitOfWork.RequestHistoryRepository.Create(history);

                List<string> ListFcmToken = await _unitOfWork.FcmTokenRepository.Get(user.Id);
                foreach (string FcmToken in ListFcmToken)
                {
                    await Utils.PushNotificationAsync(FcmToken, "Create Request", "A request with ticket ID " + entity.TicketId + " has been inserted by: " + user.FullName + " - Insert Datetime: " + DateTime.Now.ToLocalTime());
                }

                var noti = new Notification
                {
                    Title = "Create Request",
                    Content = "Create successful request with ticket ID " + entity.TicketId,
                    UserId = user.Id,
                    InsBy = user.Username,
                    UpdBy = user.Username
                };
                await _unitOfWork.NotificationRepository.Create(noti);

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


        public async Task<IEnumerable<ServiceRequestModel>> GetServiceRequestByUserId(string username)
        {
            var requests = await _unitOfWork.ServiceRequestRepository.GetByUsername(username);
            if (requests == null)
            {
                throw new AppException("Cannot find " + username);
            }
            return _mapper.Map<IEnumerable<ServiceRequestModel>>(requests);
        }

        public async Task<ServiceRequestModel> UpdateServiceRequest(string ticketId, ServiceRequestModel serviceRequest)
        {
            var entity = await _unitOfWork.ServiceRequestRepository.GetByIdToEntity(ticketId);
            if (entity != null)
            {
                var user = await _unitOfWork.UserRepository.GetByUserId(entity.UserId);
                if (serviceRequest.StaffUsername != null)
                {
                    var staff = await _unitOfWork.UserRepository.GetByUsername(serviceRequest.StaffUsername);
                    serviceRequest.StaffId = staff.Id;

                    if (entity.StaffId == null)
                    {
                        var history = new RequestHistory
                        {
                            TicketId = entity.TicketId,
                            ContentHistory = "This request has just been assigned to " + staff.FullName + " at " + DateTime.Now.ToLocalTime(),
                            UpdDatetime = DateTime.Now
                        };
                        _unitOfWork.RequestHistoryRepository.Create(history);

                        List<string> ListFcmToken = await _unitOfWork.FcmTokenRepository.Get(entity.UserId);
                        foreach (string FcmToken in ListFcmToken)
                        {
                            await Utils.PushNotificationAsync(FcmToken, "Update Staff Of Request", "This request with ticket ID  " + entity.TicketId + " has just been assigned to " + staff.FullName + " at " + DateTime.Now.ToLocalTime());
                        }

                        var noti = new Notification
                        {
                            Title = "Update Staff Of Request",
                            Content = "This request with ticket ID  " + entity.TicketId + " has just been assigned to " + staff.FullName,
                            UserId = entity.UserId,
                            InsBy = user.Username,
                            UpdBy = user.Username
                        };
                        await _unitOfWork.NotificationRepository.Create(noti);
                    }
                    else
                    {
                        var oldStaff = await _unitOfWork.UserRepository.GetByUserId(entity.StaffId);
                        var history = new RequestHistory
                        {
                            TicketId = entity.TicketId,
                            ContentHistory = "The request has just been transferred from " + oldStaff.FullName + " to " + staff.FullName + " at " + DateTime.Now.ToLocalTime(),
                            UpdDatetime = DateTime.Now
                        };
                        _unitOfWork.RequestHistoryRepository.Create(history);
                 
                        List<string> ListFcmToken = await _unitOfWork.FcmTokenRepository.Get(entity.UserId);
                        foreach (string FcmToken in ListFcmToken)
                        {
                            await Utils.PushNotificationAsync(FcmToken, "Update Staff Of Request", "The request with ticket ID " + entity.TicketId + " has just been transferred from " + oldStaff.FullName + " to " + staff.FullName + " at " + DateTime.Now.ToLocalTime());
                        }

                        var noti = new Notification
                        {
                            Title = "Update Staff Of Request",
                            Content = "The request with ticket ID " + entity.TicketId + " has just been transferred from " + oldStaff.FullName + " to " + staff.FullName,
                            UserId = entity.UserId,
                            InsBy = user.Username,
                            UpdBy = user.Username
                        };
                        await _unitOfWork.NotificationRepository.Create(noti);
                      
                    }

                }

                entity.ServiceId = serviceRequest.ServiceId != null ? serviceRequest.ServiceId : entity.ServiceId;
                entity.StaffId = serviceRequest.StaffId != null ? serviceRequest.StaffId : entity.StaffId;
                entity.Content = serviceRequest.Content != null ? serviceRequest.Content : entity.Content;
                entity.JsonInformation = serviceRequest.JsonInformation != null ? serviceRequest.JsonInformation : entity.JsonInformation;
                entity.DueDateTime = serviceRequest.DueDateTime.Year >= 1753 ? serviceRequest.DueDateTime : entity.DueDateTime;
                if (serviceRequest.Status != null && !entity.Status.Equals(serviceRequest.Status))
                {
                    var history = new RequestHistory
                    {
                        TicketId = entity.TicketId,
                        ContentHistory = "This request has just been changed from " + entity.Status + " to " + serviceRequest.Status + " at " + DateTime.Now.ToLocalTime(),
                        UpdDatetime = DateTime.Now
                    };
                    _unitOfWork.RequestHistoryRepository.Create(history);
              
                    List<string> ListFcmToken = await _unitOfWork.FcmTokenRepository.Get(entity.UserId);
                    foreach (string FcmToken in ListFcmToken)
                    {
                        await Utils.PushNotificationAsync(FcmToken, "Update Status Of Request", "This request with ticket ID " + entity.TicketId + "has just been changed from " + entity.Status + " to " + serviceRequest.Status + " at " + DateTime.Now.ToLocalTime());
                    }

                    var noti = new Notification
                    {
                        Title = "Update Status Of Request",
                        Content = "This request with ticket ID " + entity.TicketId + " has just been changed from " + entity.Status + " to " + serviceRequest.Status,
                        UserId = entity.UserId,
                        InsBy = user.Username,
                        UpdBy = user.Username
                    };
                    await _unitOfWork.NotificationRepository.Create(noti);

                    entity.Status = serviceRequest.Status != null ? serviceRequest.Status : entity.Status;
                }
                entity.UpdBy = serviceRequest.implementer != null ? serviceRequest.implementer : entity.UpdBy;
                entity.UpdDatetime = DateTime.Now;
                await _unitOfWork.Commit();
                var modelToReturn = await _unitOfWork.ServiceRequestRepository.GetByIdToModel(ticketId);
                return modelToReturn;
            }
            return null;
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
                if (item.DueDateTime > DateTime.Now && item.Status == "Expired")
                {
                    item.Status = "In-Progress";
                }
            }

            await _unitOfWork.Commit();
        }
    }
}
