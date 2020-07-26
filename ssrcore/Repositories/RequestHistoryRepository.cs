using Microsoft.EntityFrameworkCore;
using ssrcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public class RequestHistoryRepository :BaseRepository, IRequestHistoryRepository
    {
        public RequestHistoryRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void Create(RequestHistory requestHistory)
        {
            _context.RequestHistory.Add(requestHistory);
        }

        public void Delete(RequestHistory requestHistory)
        {
            _context.RequestHistory.Remove(requestHistory);
        }

        public async Task<IEnumerable<RequestHistory>> GetAll()
        {
            return await _context.RequestHistory.ToListAsync();
        }

        public async Task<RequestHistory> GetById(string ticketId)
        {
            return await _context.RequestHistory.Where(t => t.TicketId == ticketId).FirstOrDefaultAsync();
        }

    }
}
