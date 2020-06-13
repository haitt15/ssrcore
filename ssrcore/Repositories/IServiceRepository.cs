using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IServiceRepository
    {
        Task<PagedList<ServiceModel>> GetAllServices(SearchServicModel model);
        Task<Service> GetService(string serviceId);
        Task<Service> Create(ServiceModel model);
        void Update(ServiceModel model);
        Task<bool> Remove(string serviceId);
    }
}
