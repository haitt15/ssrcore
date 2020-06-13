
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<PagedList<ServiceRequestModel>> GetAllServiceRequests(SearchServiceRequestModel model);
        Task<ServiceRequest> GetServiceRequest(string userId);
        Task<ServiceRequest> Create(ServiceRequestModel model);
        void Update(ServiceRequestModel model);
        Task<bool> Remove(string ticketId);

    }
}
