using MapMaker.Models;
using Microsoft.EntityFrameworkCore;

namespace MapMaker.Data.Services
{
    public class MapsService : IMapsService
    {
        private readonly MapMakerDbContext _dbContext;
        
        public MapsService(MapMakerDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task AddAsync(Map map)
        {
            await _dbContext.Maps.AddAsync(map);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Map>> GetAllAsync()
        {
            var result = await _dbContext.Maps.ToListAsync();

            return result;
        }

        public Map GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Map Update(int id, Map map)
        {
            throw new NotImplementedException();
        }
    }
}
