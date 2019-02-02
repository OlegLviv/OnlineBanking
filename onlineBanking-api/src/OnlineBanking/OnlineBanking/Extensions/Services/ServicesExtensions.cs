using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.BLL.Services;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.DAL;

namespace OnlineBanking.Extensions.Services
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped<IEmailSendingService, EmailSendingService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ICreditCardService, CreditCardService>();
        }
    }
}
