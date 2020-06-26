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
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceModel> CreateService(ServiceModel service)
        {
            var entity = _mapper.Map<Service>(service);
            await _unitOfWork.ServiceRepository.Create(entity);
            await _unitOfWork.Commit();
            return _mapper.Map<ServiceModel>(entity);
        }

        public async Task<bool> DeleteService(string serviceId)
        {
            var service = await _unitOfWork.ServiceRepository.GetById(serviceId);
            if (service != null)
            {
                _unitOfWork.ServiceRepository.Delete(service);
                await _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public async Task<object> GetAllService(SearchServicModel model)
        {
            var services = await _unitOfWork.ServiceRepository.GetAll(model);
            dynamic result;

            List<Dictionary<string, object>> listModel = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(model.Fields))
            {
                string[] filter = model.Fields.Split(",");
                foreach (var s in services)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    for (int i = 0; i < filter.Length; i++)
                    {
                        switch (filter[i].Trim())
                        {
                            case "ServiceId":
                                dictionary.Add("ServiceId", s.ServiceId);
                                break;
                            case "ServiceNm":
                                dictionary.Add("ServiceNm", s.ServiceNm);
                                break;
                            case "DescriptionService":
                                dictionary.Add("DescriptionService", s.DescriptionService);
                                break;
                            case "FormLink":
                                dictionary.Add("FormLink", s.FormLink);
                                break;
                            case "SheetLink":
                                dictionary.Add("RoomNum", s.SheetLink);
                                break;
                            case "ProcessMaxDay":
                                dictionary.Add("ProcessMaxDay", s.ProcessMaxDay);
                                break;
                            case "DepartmentId":
                                dictionary.Add("DepartmentId", s.DepartmentId);
                                break;
                            case "DepartmentNm":
                                dictionary.Add("DepartmentNm", s.DepartmentNm);
                                break;
                        }
                    }
                    listModel.Add(dictionary);
                }
                result = listModel;
            }
            else
            {
                result = services;
            }

            return new
            {
                data = result,
                totalCount = services.TotalCount,
                totalPages = services.TotalPages
            };
        }

        public async Task<ServiceModel> GetService(string serviceId)
        {
            var entity = await _unitOfWork.ServiceRepository.GetById(serviceId);
            if (entity == null)
            {
                throw new AppException("Cannot find " + serviceId);
            }
            return _mapper.Map<ServiceModel>(entity);
        }

        public async Task<ServiceModel> UpdateService(string serviceId, ServiceModel service)
        {
            var entity = await _unitOfWork.ServiceRepository.GetById(serviceId);
            entity.ServiceNm = service.ServiceNm != null ? service.ServiceNm : entity.ServiceNm;
            entity.DepartmentId = service.DepartmentId != null ? service.DepartmentId : entity.DepartmentId;
            entity.DescriptionService = service.DescriptionService != null ? service.DescriptionService : entity.DescriptionService;
            entity.FormLink = service.FormLink != null ? service.FormLink : entity.FormLink;
            entity.SheetLink = service.SheetLink != null ? service.SheetLink : entity.SheetLink;
            if (string.IsNullOrEmpty(service.ProcessMaxDay.ToString()))
            {
                entity.ProcessMaxDay = service.ProcessMaxDay;
            } 
            entity.DelFlg = false;
            entity.UpdDatetime = DateTime.Now;
            await _unitOfWork.Commit();
            return _mapper.Map<ServiceModel>(entity);
        }
    }
}
