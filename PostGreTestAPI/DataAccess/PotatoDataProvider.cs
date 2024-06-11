using Microsoft.EntityFrameworkCore;
using PostGreTestAPI.Domain;

namespace PostGreTestAPI.DataAccess
{
    public class PotatoDataProvider : IPotatoDataProvider
    {
        private readonly ApplicationDbContext _context;

        public PotatoDataProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Potato> GetPotatoes()
        {
            return _context.potatoes.AsQueryable();
        }

        public async Task<int> GetTotalCountAsync(IQueryable<Potato> query)
        {
            return await query.CountAsync();
        }

        public async Task<List<Potato>> GetPagedPotatoesAsync(IQueryable<Potato> query, int pageNumber, int pageSize)
        {
            return await query
                .OrderBy(p => p.StartTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

    }
}
