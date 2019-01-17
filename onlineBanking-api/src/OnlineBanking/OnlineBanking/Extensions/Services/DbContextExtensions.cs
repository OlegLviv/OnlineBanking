using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace OnlineBanking.Extensions.Services
{
    public static class DbContextExtensions
    {
        public static void AddDateBaseContext(this IServiceCollection serviceCollection, IConfiguration configuration, IHostingEnvironment environment)
        {
            serviceCollection.AddDbContext<DbContext>(builder =>
            {
                builder.UseSqlServer(environment.IsDevelopment()
                    ? configuration.GetConnectionString("DevCon")
                    : configuration.GetConnectionString("ProdCon"));
            });
        }
    }
}
