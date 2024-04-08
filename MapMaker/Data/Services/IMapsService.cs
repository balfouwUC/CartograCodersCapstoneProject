using MapMaker.Models;

namespace MapMaker.Data.Services
{
    public interface IMapsService
    {
        Task<IEnumerable<Map>> GetAllAsync();
        Map GetById(int id);
        Task AddAsync(Map map); 
        Map Update(int id, Map map);
        void Delete(int id);
    }
}
