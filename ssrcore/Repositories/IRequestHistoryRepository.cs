using ssrcore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface IRequestHistoryRepository
    {
        Task<IEnumerable<RequestHistory>> GetAll();
        Task<RequestHistory> GetById(string ticketId);
        void Create(RequestHistory requestHistory);
        void Update(RequestHistory requestHistory);
        void Delete(RequestHistory requestHistory);
    }
}
