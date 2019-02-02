using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Core.Roles;
using IdentityRole = OnlineBanking.Core.Models.DomainModels.IdentityRole;

namespace OnlineBanking.DAL.Initializators
{
    public class RolesInitializer
    {
        public static async Task InitAsync(RoleManager<IdentityRole> roleManager)
        {
            if(await roleManager.Roles.AnyAsync())
                return;

            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }
    }
}
