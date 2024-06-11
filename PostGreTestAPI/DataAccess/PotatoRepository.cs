using PostGreTestAPI.Domain;
using System.Runtime.CompilerServices;

namespace PostGreTestAPI.DataAccess
{
    public class PotatoRepository : IPotatoRepository
    {
        private readonly IPotatoDataProvider _potatoDataProvider;

        public PotatoRepository(IPotatoDataProvider potatoDataProvider)
        {
            _potatoDataProvider = potatoDataProvider;
        }

        public async Task<PagedPotato<Potato>> GetPotatoesAsync(DateTime? startTime, DateTime? endTime, string status, int pageNumber, int pageSize)
        {
            // Get Query object for accessing potatoes
            var query = _potatoDataProvider.GetPotatoes();

            if (startTime.HasValue && endTime.HasValue && startTime < endTime)
            {
                query = query.Where(p => p.StartTime >= startTime.Value);
                query = query.Where(p => p.EndTime >= endTime.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.PotatoStatus == status);
            }

            var totalItems = await _potatoDataProvider.GetTotalCountAsync(query);
            var items = await _potatoDataProvider.GetPagedPotatoesAsync(query, pageNumber, pageSize);

            return new PagedPotato<Potato>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
          
        }
    }
}
