using PostGreTestAPI.Domain;

namespace PostGreTestAPI.DataAccess
{
    public interface IPotatoRepository
    {
        Task<PagedPotato<Potato>> GetPotatoesAsync(DateTime? startTime, DateTime? endTime, string status, int pageNumber, int pageSize);
    }
}
