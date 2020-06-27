
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<PagedList<ServiceRequestModel>> GetAll(SearchServiceRequestModel model);
        Task<ServiceRequestModel> GetByIdToModel(string ticketId);
        Task<ServiceRequest> GetByIdToEntity(string ticketId);
        Task<IEnumerable<ServiceRequest>> GetByUserId(int userId);
        Task Create(ServiceRequest serviceRequest);
        void Update(ServiceRequest serviceRequest);
        void Delete(ServiceRequest serviceRequest);

    }
}
