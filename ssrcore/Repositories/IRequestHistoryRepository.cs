using ssrcore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IRequestHistoryRepository
    {
        Task<IEnumerable<RequestHistory>> GetAll(string ticketId);
        void Create(RequestHistory requestHistory);
        void Delete(RequestHistory requestHistory);
    }
}
