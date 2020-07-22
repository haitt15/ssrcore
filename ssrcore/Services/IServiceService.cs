using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IServiceService
    {
        object GetAllService(SearchServicModel model, IEnumerable<ServiceModel> list);
        Task<IEnumerable<ServiceModel>> GetServices();
        Task<ServiceModel> GetService(string serviceId);
        ServiceModel GetServiceByRedis(string serviceId, IEnumerable<ServiceModel> list);
        Task<ServiceModel> CreateService(ServiceModel service);
        Task<bool> DeleteService(string serviceId);
        Task<ServiceModel> UpdateService(string serviceId, ServiceModel service);
    }
}
