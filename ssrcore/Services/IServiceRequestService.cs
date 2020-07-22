using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IServiceRequestService
    {
        Task<object> GetAllServiceRequest(SearchServiceRequestModel model);
        Task<ServiceRequestModel> GetServiceRequest(string ticketId);
        Task<IEnumerable<ServiceRequestModel>> GetServiceRequestByUserId(int userId);
    
        Task<ServiceRequestModel> CreateServiceRequest(ServiceRequestModel serviceRequest);
        Task<bool> DeleteServiceRequest(string ticketId);
        Task<ServiceRequestModel> UpdateServiceRequest(string ticketId, ServiceRequestModel serviceRequest);
        Task UpdateStatusExpiredServiceRequest();
    }
}
