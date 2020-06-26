using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IServiceService
    {
        Task<Object> GetAllService(SearchServicModel model);
        Task<ServiceModel> GetService(string serviceId);
        Task<ServiceModel> CreateService(ServiceModel service);
        Task<bool> DeleteService(string serviceId);
        Task<ServiceModel> UpdateService(string serviceId, ServiceModel service);
    }
}
