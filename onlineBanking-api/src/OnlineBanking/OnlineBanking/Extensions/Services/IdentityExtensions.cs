﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.BLL.Providers;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.DAL;

namespace OnlineBanking.Extensions.Services
{
    public static class IdentityExtensions
    {
        public static void AddIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<User, Core.Models.DomainModels.IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<DataBaseContext>()
                .AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleManager<RoleManager<Core.Models.DomainModels.IdentityRole>>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<UserTwoFactorTokenProvider>(ProviderConstansts.UserTwoFactorTokenProvider);
        }
    }
}
