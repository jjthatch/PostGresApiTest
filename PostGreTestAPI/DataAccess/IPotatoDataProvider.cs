using PostGreTestAPI.Domain;

namespace PostGreTestAPI.DataAccess
{
    public interface IPotatoDataProvider
    {
        IQueryable<Potato> GetPotatoes();
        Task<int> GetTotalCountAsync(IQueryable<Potato> query);
        Task<List<Potato>> GetPagedPotatoesAsync(IQueryable<Potato> query, int pageNumber, int pageSize);
    }
}
