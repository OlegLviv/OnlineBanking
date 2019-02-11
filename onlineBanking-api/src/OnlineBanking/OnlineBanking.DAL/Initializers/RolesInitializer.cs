using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.Core.Roles;
using OnlineBanking.DAL.Initializers.Abstract;
using IdentityRole = OnlineBanking.Core.Models.DomainModels.IdentityRole;

namespace OnlineBanking.DAL.Initializers
{
    public class RolesInitializer : IInitializer
    {
        public async Task InitAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (await roleManager.Roles.AnyAsync())
                    return;

                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
        }

        public int Priority => 1;
    }
}
