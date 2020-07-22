using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IRequestHistoryService
    {
        Task<IEnumerable<RequestHistoryModel>> GetAllRequestHistory();
        Task<RequestHistoryModel> GetRequestHistory(string ticketId);
        Task<RequestHistoryModel> CreateRequestHistory(RequestHistoryModel model);
    }
}
