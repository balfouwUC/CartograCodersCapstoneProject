using MapMaker.Models;

namespace MapMaker.Data
{
    public class MapMakerDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<MapMakerDbContext>();

                context.Database.EnsureCreated();

                if (!context.Users.Any()) 
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            Name = "Test User 1"
                        },
                        new User()
                        {
                            Name = "Test User 2"
                        }
                    });
                    context.SaveChanges();
                }
                if (!context.Maps.Any())
                {
                    context.Maps.AddRange(new List<Map>()
                    {
                        new Map()
                        {
                            Name = "Test Map 1",
                            CreatedBy = 0,
                            Data = "-1"
                        },
                        new Map()
                        {
                            Name = "Test Map 2",
                            CreatedBy = 1,
                            Data = "-1"
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
