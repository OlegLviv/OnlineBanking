using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.Core.Models.DomainModels;

namespace OnlineBanking.Extensions.Services
{
    public static class IdentityExtensions
    {
        public static void AddIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<User, Core.Models.DomainModels.IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<DbContext>()
                .AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleManager<RoleManager<Core.Models.DomainModels.IdentityRole>>()
                .AddDefaultTokenProviders();
        }
    }
}
