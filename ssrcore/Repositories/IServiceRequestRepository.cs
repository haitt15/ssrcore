
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<PagedList<ServiceRequestModel>> GetAllServiceRequests(SearchServiceRequestModel model);
        Task<ServiceRequestModel> GetServiceRequestModel(string ticketId);

        Task<ServiceRequest> GetServiceRequest(string ticketId);
        Task<ServiceRequestModel> Create(ServiceRequestModel model);
        Task<ServiceRequestModel> Update(string ticketId, ServiceRequestModel model);
        Task<bool> Remove(string ticketId);

    }
}
