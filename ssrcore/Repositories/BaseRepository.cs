

using ssrcore.Models;

namespace ssrcore.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
