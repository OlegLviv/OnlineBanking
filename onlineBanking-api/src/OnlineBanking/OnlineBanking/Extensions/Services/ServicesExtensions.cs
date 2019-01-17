using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.DAL;

namespace OnlineBanking.Extensions.Services
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
