using ssrcore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface IRequestHistoryService
    {
        Task<IEnumerable<RequestHistoryModel>> GetAllRequestHistory();
        Task<RequestHistoryModel> GetRequestHistory(string ticketId);
    }
}
