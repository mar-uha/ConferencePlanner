using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            => services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=conferences.db"));

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}