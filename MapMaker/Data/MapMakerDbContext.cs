using MapMaker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MapMaker.Data
{
    public class MapMakerDbContext:DbContext
    {
        public MapMakerDbContext(DbContextOptions<MapMakerDbContext> options) : base(options)
        {

        }

        public DbSet<Map> Maps { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
