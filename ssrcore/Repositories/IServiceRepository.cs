using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IServiceRepository
    {
        Task<PagedList<ServiceModel>> GetAll(SearchServicModel model);
        Task<ServiceModel> GetByIdToModel(string serviceId);
        Task<Service> GetByIdToEntity(string serviceId);
        Task Create(Service service);
        void Update(Service service);
        void Delete(Service service);
    }
}
