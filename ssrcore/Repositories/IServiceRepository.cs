using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IServiceRepository
    {
        PagedList<ServiceModel> GetAll(SearchServicModel model, IEnumerable<ServiceModel> listServices);
        Task<IEnumerable<ServiceModel>> GetServices();
        ServiceModel GetByIdRedis(string serviceId, IEnumerable<ServiceModel> listServices);
        Task<ServiceModel> GetByIdToModel(string serviceId);
        Task<Service> GetByIdToEntity(string serviceId);
        Task Create(Service service);
        void Update(Service service);
        void Delete(Service service);
    }
}
