using System.Configuration;
using MapMaker.Models;

namespace MapMaker
{
    public class Startup
    {
        public IConfigurationRoot? Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.Add(new ServiceDescriptor(typeof(MapMakerContext), new MapMakerContext(Configuration.GetConnectionString("DefaultConnection"))));
        }

        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
